![http://qdbe.rgr.jp/qdbeImage/header.png](http://qdbe.rgr.jp/qdbeImage/header.png)

# What's quickDBExplorer? #
### **quickDBExplorer は Microsoft 社のSQL SERVER (2014/2012/2008R2/2008/2005/2000)を利用している開発者向けの補助ツールです** ###

.NET Framework 3.5上で動作する、データベースアプリケーションの開発者向けのMDI形式の補助ツールです。エンタープライズマネージャーやクエリアナライザでは用意されていない機能を実現したり、使いにくい機能を補助する目的に作成されました(完全な置き換えは目標としていません)。
特にテーブルに対する処理に特化しています。

テーブル/View/Synonymを指定した

  * insert文の生成(insert文によるデータエクスポート機能)
  * フィールドのリストの生成
  * Select 文の生成
  * 簡易的なCreate 文の生成
  * CSV,TAB区切り形式でのデータ抽出＆読込
  * INDEX情報の表示
  * データの表示＆編集
  * フィールド一覧の表示
  * データ件数の取得
  * データ依存関係の取得
  * フィールド情報を元にしたwhere句の生成

等が可能です。

結果は、クリップボードもしくはファイルへ書き出されます

上記以外にもクエリアナライザ等の起動が可能です。

# 動作環境 #

  * .NET Framework 3.5 SP1 がインストールされているWindowsマシン
  * 画面解像度 が　1024 x 768 あることを想定して画面デザインしています
  * 対象としているデータベースエンジンは SQL SERVER 2014/2012/2008R2/2008/2005/2000

# Announce! #
> 2.3.0 Release版のダウンロードが可能になりました。
> 最新の更新情報・変更情報は[こちら](Announce.md)

# Documents #
  * [開発環境](DevelopmentEnvironment.md)
  * [Release 2.3.0 用ヘルプファイル](http://qdbe.rgr.jp/help/2.3.0/quickDBExplorerHelp.htm)
  * [Release 2.2.0 用ヘルプファイル](http://qdbe.rgr.jp/help/2.2.0/quickDBExplorerHelp.htm)
  * [Release 2.1.0 用ヘルプファイル](http://qdbe.rgr.jp/help/2.1.0/quickDBExplorerHelp.htm)
  * [Release 2.0.1 用ヘルプファイル](http://qdbe.rgr.jp/help/2.0.1/quickDBExplorerHelp.htm)
  * [Release 2.0.0 用ヘルプファイル](http://qdbe.rgr.jp/help/2.0.0/quickDBExplorerHelp.htm)
  * [Release 1.5.0 用ヘルプファイル](http://qdbe.rgr.jp/help/1.5.0/quickDBExplorerHelp.htm)

# ScreenShot #
![![](http://qdbe.rgr.jp/qdbeImage/ScreenShotSmall.png)](http://qdbe.rgr.jp/qdbeImage/ScreenShot.png)

# その他公開場所 #
ここでも公開しています [Vector](http://www.vector.co.jp/soft/winnt/business/se431473.html)


---


Front End tool for Microsoft SQL Server 2014/2012/2008R2/2008/2000/2005

this tool can follows
  * make insert script from data
  * make csv/tsv files from data
  * load data to table from file or Clipboard
  * make field list
  * view & edit table data quickly
  * view index information
  * etc...
These results are output to a clipboard or a file.

Run under .NET Framework 3.5 SP1