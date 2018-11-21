# devil-san-unity-assistance

### 開発方針
- Workspaceフォルダは何でも入れてOK(開発環境)
- そこから上はちゃんとする
- Pluginsは悩み中
  - Venderを切って、こっちは外部依存アセット、自分のアセットはもう1個パス切るが良さそう
  - 今は平展開してるのでPackage化するのがめんどい

### TODO
- そのうちWikiに書いたほうがいい気がする

### AutoSerializeFieldLinker
- いずれかのEditorフォルダの下に配置してください
- Hierarchyの右クリックメニューの一番下のほうに「AutoLink」が出ます
- 右クリックで指定したオブジェクトにアタッチされたコンポーネントのSeiralizeFieldに同じ名前と同じコンポーネントが子要素に見つかったらリンクしてくれます

### Stencil Shader
- UI背景(Image)の上に出るけど一定領域でマスクされたいパーティクルをマスクするシェーダー
- ステンシルバッファを使ってる。既存シェーダーにstencil設定を追加するだけ
- ステンシル設定に応じてマスクするマスク画像用シェーダーが1PASSに1個必要
