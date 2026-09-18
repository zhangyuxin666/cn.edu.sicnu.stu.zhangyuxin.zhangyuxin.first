using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

var root = Directory.GetCurrentDirectory();
var source = Path.Combine(root, "实验一+多语言版本Hello+World.docx");
var output = Path.Combine(root, "实验报告_张雨鑫_2024110548.docx");
File.Copy(source, output, true);

using var doc = WordprocessingDocument.Open(output, true);
var body = doc.MainDocumentPart!.Document.Body!;
var paragraphs = body.Elements<Paragraph>().ToList();

static void Fill(Paragraph paragraph, string content, bool heading = false)
{
    foreach (var node in paragraph.ChildElements.Where(x => x is not ParagraphProperties).ToList()) node.Remove();
    var runProps = new RunProperties(new RunFonts { Ascii = "Calibri", HighAnsi = "Calibri", EastAsia = "SimSun" });
    if (heading) runProps.Append(new Bold());
    runProps.Append(new FontSize { Val = heading ? "24" : "21" });
    paragraph.Append(new Run(runProps, new Text(content) { Space = SpaceProcessingModeValues.Preserve }));
    var pPr = paragraph.ParagraphProperties ?? paragraph.PrependChild(new ParagraphProperties());
    pPr.SpacingBetweenLines = new SpacingBetweenLines { After = "80", Line = "276", LineRule = LineSpacingRuleValues.Auto };
}

Fill(paragraphs[0], "实验编号：1    Android移动应用开发实验报告                         2026年9月18日", true);
Fill(paragraphs[1], "计算机科学学院                  实验名称：多语言版本 Hello World");
Fill(paragraphs[2], "姓名：张雨鑫      学号：2024110548      指导老师：李贵洋      实验成绩：________");
Fill(paragraphs[5], "GitHub网址：https://github.com/zhangyuxin666/cn.edu.sicnu.stu.zhangyuxin.zhangyuxin.first");

string[] implementation = [
    "一、开发环境与方案", 
    "开发工具：Android Studio；构建环境：JBR 25、Gradle 9.5.0、Android Gradle Plugin 9.3.0；Android SDK Platform API 37、Build Tools 36.0.0。", 
    "创建原生 Java Android 工程，包名 edu.sicnu.multilingualhello；界面全部在 MainActivity.buildScreen() 中用 ScrollView、LinearLayout、TextView、ImageView 和 Button 创建，没有使用 res/layout 布局文件。", 
    "应用标题设置为“张雨鑫2024110548”；使用 adaptive icon，自绘对白框和彩色条纹，替换默认启动图标。", 
    "二、三语交互核心代码与说明", 
    "private static final String[] GREETINGS = {\"你好，世界！\", \"Hello, World!\", \"こんにちは、世界！\"};", 
    "private static final int[] FLAGS = {R.drawable.flag_china, R.drawable.flag_america, R.drawable.flag_japan};", 
    "button.setOnClickListener(v -> showLanguage(index));  // 按钮点击时切换语言", 
    "showLanguage(index) 同步调用 setImageResource、setText 和 setContentDescription，更新国旗、问候语与无障碍说明；当前语言按钮禁用，避免重复点击。", 
    "中国、美国国旗使用提供的图片；第三语种选日语，日本国旗 SVG 从 Wikimedia Commons 下载，按原尺寸及颜色转换为 PNG 后放入 drawable-nodpi。", 
    "三、构建与调试记录", 
    "首次构建报错：SDK location not found。原因是命令行缺少 ANDROID_HOME；设置为 C:\\Users\\zyx15\\AppData\\Local\\Android\\Sdk 后重新构建。", 
    "执行 :app:assembleDebug，结果 BUILD SUCCESSFUL；生成 app/build/outputs/apk/debug/app-debug.apk。", 
    "ADB 已执行 adb devices -l；返回 List of devices attached，但没有设备。当前尚未创建 AVD，也没有连接手机。", 
    "待设备接入后执行：adb install -r app-debug.apk；adb shell am start -n edu.sicnu.multilingualhello/.MainActivity；adb shell ls /；adb shell input tap X Y。", 
    "运行界面截图和模拟触摸截图：待在模拟器或真机上完成验证后补入。本报告没有将构建结果冒充为运行截图。", 
    "四、代码和资源位置：app/src/main/java/edu/sicnu/multilingualhello/MainActivity.java；app/src/main/res/drawable-nodpi；app/src/main/res/mipmap-anydpi-v26。"
];
for (var i = 0; i < implementation.Length; i++) Fill(paragraphs[32 + i], implementation[i], i is 0 or 4 or 10);

string[] analysis = [
    "一、预期结果：启动后默认显示中国国旗和“你好，世界！”；点击 English 显示美国国旗与“Hello, World!”；点击 日本語 显示日本国旗与“こんにちは、世界！”。", 
    "二、静态检查：代码中三组国旗、问候语和国家说明按同一索引映射；三个按钮都绑定 showLanguage(index)；AndroidManifest.xml 已声明可启动的 MainActivity。", 
    "三、构建检查：Debug APK 构建成功，可证明 Java 编译、资源编译和打包通过；但不能单凭此证明设备上的显示和触摸行为正确。", 
    "四、设备验证状态：adb devices 没有列出任何设备；因此安装、启动、根目录列表、模拟点击和实际运行截图尚未完成。", 
    "五、问题分析：本机已安装 Android SDK 的平台、构建工具、platform-tools 和 emulator 程序，但缺少系统镜像和 AVD；应在 Android Studio 的 Device Manager 创建虚拟设备并下载镜像。", 
    "六、后续验证：启动 AVD；运行 adb devices 确认状态为 device；安装 APK；启动 Activity；执行 adb shell ls /；用 uiautomator dump 确认按钮边界后执行 input tap；截图并对照三语输出。", 
    "七、质量评价：纯代码界面、三语交互、三面国旗、自定义图标与姓名学号标题均已实现；设备实测和截图仍是待完成项。", 
    "八、仓库说明：按本人选择使用 GitHub 而非题目要求的 Gitee。若教师严格按 Gitee 检查，应另建 Gitee 仓库并替换报告链接。"
];
for (var i = 0; i < analysis.Length; i++) Fill(paragraphs[50 + i], analysis[i]);
foreach (var index in Enumerable.Range(58, 7).Reverse()) paragraphs[index].Remove();
doc.MainDocumentPart.Document.Save();
Console.WriteLine(output);
