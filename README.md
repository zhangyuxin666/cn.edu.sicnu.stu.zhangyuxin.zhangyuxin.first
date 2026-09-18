# 多语言 Hello World 实验

Android 原生 Java 应用，界面完全由 `MainActivity.java` 动态创建，不使用 XML 布局文件。点击中文、English、日本語按钮，切换问候语及对应国旗；应用图标为自定义 adaptive icon。

## 环境与构建

- Android Studio 与 Android SDK
- Android Gradle Plugin 9.3.0、Gradle 9.5.0、JDK 17 或更高版本
- SDK Platform API 37、Build Tools 36.0.0

在 Android Studio 中打开本目录并运行 `app`，或在命令行执行 `gradle :app:assembleDebug`。APK 位于 `app/build/outputs/apk/debug/app-debug.apk`。

## ADB 验证步骤

请先启动 Android 模拟器或连接已开启 USB 调试的手机，然后运行：

```powershell
adb devices
adb install -r app/build/outputs/apk/debug/app-debug.apk
adb shell am start -n edu.sicnu.multilingualhello/.MainActivity
adb shell ls /
adb shell input tap 180 750
adb shell input tap 360 750
```

触摸坐标需要根据设备分辨率及实际按钮位置调整；可用 `adb shell wm size` 查看分辨率、`adb shell uiautomator dump` 查找按钮区域。报告中应附真实设备截图，不能用构建成功截图替代运行截图。

第三面日本国旗参考 [Wikimedia Commons / Flag of Japan](https://commons.wikimedia.org/wiki/File:Flag_of_Japan.svg)，下载的 SVG 源文件为 `flag_japan_source.svg`，APK 中使用按源文件尺寸与颜色转换的 PNG。
