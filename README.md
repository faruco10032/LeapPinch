# LeapPinch

UnityとLeapmotionで物体をつまむ．

# 環境
* Unity:2018.4.10f1
* Leap Motion Core Version:4.4.0
* Leap_Motion_Developer_Kit_4.0.0+52173

# 使い方
## 概要
Leapmotionの指でものをつまめるようにします．

接触判定をしたいコライダーを設定し、接触オブジェクトの座標を認識し接触オブジェクトの座標を接触判定コライダーの中心に移動させます．

移動させる際は移動中心座標を接触オブジェクトの中心座標に依存しないように空のGameobjectを生成し接触オブジェクトをその子にすることで移動させます．

isEnablePinchタグを持っている物体のみつまめます．

コライダーはTriggerにしておいてください．


## 導入

