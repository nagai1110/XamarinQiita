# XamarinQiita
XamarinFormsで作ったQiitaアプリ

## Description
Xamarinでモバイルアプリをどのように作れるかを調査するために作ったサンプルプロジェクト   
C#良く分かっていないので糞コードです   

##　NameSpaceの説明
### Qiita.API
QiitaのAPI周り   
[Qiita API v2](https://qiita.com/api/v2/docs)

#### QiitaAPI
```
GetAccessToken : アクセストークンの取得
AuthenticatedUser : アクセストークンのチェック？(忘れました)
GetAllItems : 新着記事の取得
```

#### QiitaAccessToken
アクセストークンの情報を保持するクラス

#### QiitaGroup
グループ情報を保持するクラス

#### QiitaItem
記事情報を保持するクラス

#### QiitaUser
ユーザー情報を保持するクラス

### Qiita.API.OAuth
Qiitaの認証(OAuth)周り

#### QiitaAuthenticator
OAuth認証を行うクラス   
内部で、Xamarin.Formsの`DependencyService`を使って、Android/iOS独自コードを呼び出す、という仕組みを使っています

#### IQiitaOAuth
Android/iOS独自部分のI/Fクラス   
`Qiita.API.OAuth.Android`の`QiitaOAuth_Android`がAndroidの具象クラスになります   
※すみません、iOSは未実装です

### Qiita.Http
HTTPリクエスト周り

#### HttpRequest
```
GET : Getリクエスト
POST : Postリクエスト
```

### Qiita.Page
UI周り

#### MainPage
メインページ(MasterDetailPage)

#### MenuItemPage
メインページのMasterPageに表示するページ

- ログイン中ユーザーのアイコン表示
- ログイン/ログアウト

#### ItemListPage
メインページのDetailPageに表示するページ

- 記事一覧の表示
- 記事をタップするとアクションシートがでてきて、記事の閲覧 or 外部アプリへの共有を選択できる

#### WebViewPage
記事内容を表示するページ(ただのWebView)

### Qiita.Setting
データの永続化周り

#### PropertiesKey
永続化するキーを定義するクラス

#### PropertiesAccesser
永続化データにアクセスするクラス

```
Get : 取得
Set : 更新
Remove : 削除
```

### Qiita.Share
外部アプリへの共有周り

#### Share
外部アプリへの共有を行うクラス   
ほぼOSS


