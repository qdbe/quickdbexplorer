/*---------------------------------------------
		quickDBExplorer
		
		Authoer : Y.N
		URL:	http://qdbe.rgr.jp/
		mailto: accept　＠　qdbe　.　rgr　.　jp　(文字を半角にし、かつ全角スペースを削除してください)
----------------------------------------------*/


1.1.	quickDBExplorer とは
.NET Framework 3.5.1上で動作する、データベース(SQL SERVER 2016/2014/2012/2008R2/2008/2005/2000)アプリケーションの開発者向けのMDI形式の補助ツールです。エンタープライズマネージャー/SQL Server Management Studioやクエリアナライザでは用意されていない機能を実現したり、使いにくい機能を補助する目的に作成されました(完全な置き換えは目標としていません)。
特にテーブルに対する処理に特化しています。
テーブルを指定した
・	insert文の生成(insert文によるデータエクスポート機能)
・	フィールドのリストの生成
・	Select 文の生成
・	簡易的なCreate 文の生成
・	CSV,TAB区切り形式でのデータ抽出&読込
・	INDEX情報の表示
・	データの表示＆編集
・	フィールド一覧の表示
・	データ件数の取得
・	データ依存関係の取得
・	フィールド情報を元にしたwhere句の生成
等が可能です。
結果は、クリップボードもしくはファイルへ書き出されます
上記以外にもManagement Studio等の起動が可能です。

1.2.	動作環境
・	.NET Framework 3.5(Service Pack 1) がインストールされているWindowsマシン
・	画面解像度 が　1024 x 768 あることを想定して画面デザインしています
・	対象としているデータベースエンジンは SQL SERVER 2016/2014/2012/2008R2/2008/2005/2000 

2.	利用方法

2.1.	起動方法
・	quickDBExplorer を起動します。
・	起動後には下記のようなウィンドウが表示されます。このウィンドウを閉じた場合、コントロールキー＋Nキーを押下するか、メニューから「接続」―「新規接続」を選択することで同じ画面を表示することが可能です。


ヘルプについては、インストール後の quickDBExplorerHelp.htm を参照してください。


3.	インストール・アンインストール

3.1.		インストール方法
	以前のバージョンを利用している場合には、コントロールパネル-プログラムの追加と削除 からquickDBExplorerを選択し、一旦アンインストールするか、展開後のファイルで上書きしてください。
	quickDBExplorer.2.4.0.zip を任意の場所に解凍して下さい。
	Ver 2.0 以降はレジストリなどは利用していません。

3.2.		アンインストール方法
	インストール時に展開したファイル、およびquickDBExplorer.xml(存在する場合のみ)を削除していただくだけで結構です。
	レジストリは利用していません。


4.	ライセンス等について

4.1		ライセンス等の詳細

Copyright (C) 2004-2016　Y.N(godz)

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.



このプログラムはフリーソフトウェアです。あなたはこれを、フリーソフトウェ
ア財団によって発行された GNU 一般公衆利用許諾契約書(バージョン2か、希
望によってはそれ以降のバージョンのうちどれか)の定める条件の下で再頒布
または改変することができます。

このプログラムは有用であることを願って頒布されますが、*全くの無保証* 
です。商業可能性の保証や特定の目的への適合性は、言外に示されたものも含
め全く存在しません。詳しくはGNU 一般公衆利用許諾契約書をご覧ください。
 
あなたはこのプログラムと共に、GNU 一般公衆利用許諾契約書の複製物を一部
受け取ったはずです。もし受け取っていなければ、フリーソフトウェア財団ま
で請求してください(宛先は the Free Software Foundation, Inc., 59
Temple Place, Suite 330, Boston, MA 02111-1307 USA)。



