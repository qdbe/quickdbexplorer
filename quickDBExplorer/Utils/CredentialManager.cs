using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Security.Authentication;



using DWORD = System.UInt32;
using LPTSTR = System.String;
using LPBYTE = System.String;
using LPCTSTR = System.String;
using BOOL = System.Boolean;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;


namespace quickDBExplorer.Utils
{


    public enum CRED_TYPE
    {
        GENERIC = 1,
        DOMAIN_PASSWORD,
        DOMAIN_CERTIFICATE,
        DOMAIN_VISIBLE_PASSWORD,
        DOMAIN_EXTENDED
    }

    [Flags]
    public enum CRED_FLAGS
    {
        PROMPT_NOW = 0x2,
        USERNAME_TARGET = 0x4
    }

    public enum CRED_PERSIST
    {
        SESSION = 1,
        LOCAL_MACHINE,
        ENTERPRISE
    }
    #region internal use

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct PCREDENTIAL
    {
        internal DWORD Flags;
        internal DWORD Type;
        internal LPTSTR TargetName;
        internal LPTSTR Comment;
        internal System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
        internal DWORD CredentialBlobSize;
        internal IntPtr CredentialBlob;
        internal DWORD Persist;
        internal DWORD AttributeCount;
        internal IntPtr Attributes;
        internal LPTSTR TargetAlias;
        internal LPTSTR UserName;
    }

    static class Natives
    {
        [DllImport("Advapi32.dll", EntryPoint = "CredDeleteW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern BOOL CredDelete(LPCTSTR TargetName, DWORD Type, DWORD Flags);

        [DllImport("Advapi32.dll", EntryPoint = "CredFree", SetLastError = true)]
        internal static extern void CredFree(IntPtr cred);

        [DllImport("Advapi32.dll", EntryPoint = "CredReadW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern BOOL CredRead(LPCTSTR TargetName, DWORD Type, DWORD Flags, out IntPtr Credential);

        [DllImport("Advapi32.dll", EntryPoint = "CredWriteW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern BOOL CredWrite(ref PCREDENTIAL Credential, DWORD Flags);

        [DllImport("kernel32.dll", EntryPoint = "FormatMessage", CharSet = CharSet.Unicode)]
        internal static extern DWORD FormatMessage(DWORD dwFlags, IntPtr lpSource, DWORD dwMessageId, DWORD dwLanguageId, out LPTSTR lpBuffer, DWORD nSize, IntPtr Arguments);
    }


    #endregion

    public static class CredManager
    {
        public struct Credential
        {
            public string UserName;
            public string Password;
        }

        public static Credential Read(string targetname)
        {
            IntPtr handle = IntPtr.Zero;
            try
            {
                Credential result = new Credential();
                if (Natives.CredRead(targetname, (DWORD)CRED_TYPE.GENERIC, 0, out handle))
                {
                    PCREDENTIAL cred = (PCREDENTIAL)Marshal.PtrToStructure(handle, typeof(PCREDENTIAL));

                    // NativeCallから返ってきたポインタがそのまま使われる環境が存在する為コピーする。
                    result.UserName = String.Copy(cred.UserName);
                    // 自動的にマーシャルさせると、
                    // サイズを考慮せずに連続領域を全て持ってきてしまうらしく、末尾にtargetnameが付いてくる。
                    var s = Marshal.PtrToStringUni(cred.CredentialBlob, (int)cred.CredentialBlobSize / 2);
                    result.Password = Decrypt(s);

                }
                else
                {
                    return new Credential();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    try
                    {
                        Natives.CredFree(handle);
                    }
                    catch (Exception exp)
                    {
                        ThrowWin32Exception();
                    }
                }
            }
        }

        public static void Write(string targetname, Credential c)
        {
            try
            {
                PCREDENTIAL cred = new PCREDENTIAL();
                cred.TargetName = targetname;
                cred.UserName = c.UserName;
                // 資格情報マネージャに格納されたパスワードはある種のツールで中身を見る事が出来るので暗号化する。
                var s = Encrypt(c.Password);
                cred.CredentialBlob = Marshal.StringToCoTaskMemUni(s);
                cred.CredentialBlobSize = (DWORD)Encoding.Unicode.GetBytes(s).Length;
                cred.Type = (DWORD)CRED_TYPE.GENERIC;
                cred.Persist = (DWORD)CRED_PERSIST.LOCAL_MACHINE;
                cred.AttributeCount = 0;
                cred.Attributes = IntPtr.Zero;
                cred.Comment = null;
                cred.LastWritten = DateTime.Today.ToFILETIME();

                if (Natives.CredWrite(ref cred, 0) == false)
                {
                    ThrowWin32Exception();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }


        public static string Encrypt(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            var b = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(b);
        }
        public static string Decrypt(string s)
        {
            try
            {
                var bytes = Convert.FromBase64String(s);
                var b = ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser);
                return Encoding.Unicode.GetString(b);
            }
            catch (CryptographicException)
            {
                // 資格情報マネージャで値を変更している等、複合化出来ない要因があるので、
                // 値が無効であるとみなす。
                return null;
            }
        }

        internal static void ThrowWin32Exception()
        {
            int code = Marshal.GetLastWin32Error();
            const DWORD FORMAT_MESSAGE_FROM_SYSTEM = 0x00001000;
            string msg;
            Natives.FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, (uint)code, 0, out msg, 0, IntPtr.Zero);
            throw new Exception(msg);
        }

        internal static System.Runtime.InteropServices.ComTypes.FILETIME ToFILETIME(this DateTime datetime)
        {
            var value = datetime.ToFileTime();
            return new System.Runtime.InteropServices.ComTypes.FILETIME() { dwHighDateTime = unchecked((int)(value >> 32)), dwLowDateTime = unchecked((int)value) };
        }

    }     
}
