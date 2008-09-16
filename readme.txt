quickDBExplorer は Microsoft 社のSQL SERVER (2008/2005/2000)を利用している開発者向けの補助ツールです

.NET Framework 2.0上で動作する、データベースアプリケーションの開発者向けのMDI形式の補助ツールです。エンタープライズマネージャーやクエリアナライザでは用意されていない機能を実現したり、使いにくい機能を補助する目的に作成されました(完全な置き換えは目標としていません)。特にテーブルに対する処理に特化しています。

テーブル/View/Synonymを指定した

    * insert文の生成(insert文によるデータエクスポート機能)
    * フィールドのリストの生成
    * Select 文の生成
    * 簡易的なCreate 文の生成
    * CSV,TAB区切り形式でのデータ抽出＆読込(読込は1.4.6より)
    * INDEX情報の表示
    * データの表示＆編集
    * フィールド一覧の表示
    * データ件数の取得
    * データ依存関係の取得 

等が可能です。

結果は、クリップボードもしくはファイルへ書き出されます

上記以外にもクエリアナライザ等の起動が可能です。
動作環境

    * .NET Framework 2.0 SP1 がインストールされているWindowsマシン
    * 画面解像度 が　1024 x 768 あることを想定して画面デザインしています
    * 対象としているデータベースエンジンは SQL SERVER 2008/2005/2000 
    


Front End tool for Microsoft SQL Server 2000/2005/2008

this tool can follows

    * make insert script from data
    * make csv/tsv files from data
    * load data to table from file or Clipboard
    * make field list
    * view & edit table data quickly
    * view index information
    * etc... 

These results are output to a clipboard or a file.

Run under .NET Framework 2.0 

