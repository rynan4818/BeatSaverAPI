# BeatSaverAPI

BeatSaver のAPIがcURLでアクセス出来なくなったため、BeatSaberSharpで情報取得するコマンドラインツール。

## 使い方
```
BeatSaverAPI.exe 譜面ハッシュ値又はkey [タイムアウト:デフォルト10秒]
```
結果はJSONフォーマットで標準出力されるので、リダイレクトやパイプで使用して下さい。
難易度などは出力されず、出力する情報は絞っています。