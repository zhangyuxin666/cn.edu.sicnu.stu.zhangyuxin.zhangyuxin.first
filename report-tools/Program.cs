using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

var root = Directory.GetCurrentDirectory();
var source = Path.Combine(root, "实验一+多语言版本Hello+World.docx");
var output = Path.Combine(root, "实验报告_张雨鑫_2024110548.docx");
File.Copy(source, output, true);

using var doc = WordprocessingDocument.Open(output, true);
var body = doc.MainDocumentPart!.Document!.Body!;
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
    "创建原生 Java Android 工程，包名 cn.edu.sicnu.stu.zhangyuxin.first；界面全部在 MainActivity.buildScreen() 中用 ScrollView、LinearLayout、TextView、ImageView 和 Button 创建，没有使用 res/layout 布局文件。",
    "桌面应用名与页面顶部标题均设置为“张雨鑫2024110548”（title.setText(R.string.app_name)）；使用 adaptive icon，自绘对白框和彩色条纹，替换默认启动图标。", 
    "二、三语交互核心代码与说明", 
    "private static final String[] GREETINGS = {\"你好，世界！\", \"Hello, World!\", \"こんにちは、世界！\"};", 
    "private static final int[] FLAGS = {R.drawable.flag_china, R.drawable.flag_america, R.drawable.flag_japan};", 
    "button.setOnClickListener(v -> showLanguage(index));  // 按钮点击时切换语言", 
    "showLanguage(index) 同步调用 setImageResource、setText 和 setContentDescription，更新国旗、问候语与无障碍说明；当前语言按钮禁用，避免重复点击。", 
    "中国、美国国旗使用提供的图片；第三语种选日语，日本国旗 SVG 从 Wikimedia Commons 下载，按原尺寸及颜色转换为 PNG 后放入 drawable-nodpi。", 
    "三、构建与调试记录", 
    "首次构建报错：SDK location not found。原因是命令行缺少 ANDROID_HOME；设置为 C:\\Users\\zyx15\\AppData\\Local\\Android\\Sdk 后重新构建。", 
    "执行 :app:assembleDebug，结果 BUILD SUCCESSFUL；生成 app/build/outputs/apk/debug/app-debug.apk。", 
    "已安装 Android 35 x86_64 系统镜像并创建 HelloWorld_API35 模拟器；adb devices -l 显示 emulator-5554，状态为 device。", 
    "adb install -r app-debug.apk 返回 Success；adb shell am start -n cn.edu.sicnu.stu.zhangyuxin.first/.MainActivity 成功启动；adb shell ls / 输出 acct、data、system 等根目录项。",
    "根据 uiautomator dump 的按钮边界，用 adb shell input tap 540 1340 切到英语，再以 860 1340 切到日语；截图及界面 XML 均已保存到 evidence 目录。", 
    "四、代码和资源位置：app/src/main/java/cn/edu/sicnu/stu/zhangyuxin/first/MainActivity.java；app/src/main/res/drawable-nodpi；app/src/main/res/mipmap-anydpi-v26。"
];
for (var i = 0; i < implementation.Length; i++) Fill(paragraphs[32 + i], implementation[i], i is 0 or 4 or 10);

string[] analysis = [
    "一、预期结果：启动后默认显示中国国旗和“你好，世界！”；点击 English 显示美国国旗与“Hello, World!”；点击 日本語 显示日本国旗与“こんにちは、世界！”。", 
    "二、静态检查：代码中三组国旗、问候语和国家说明按同一索引映射；三个按钮都绑定 showLanguage(index)；AndroidManifest.xml 已声明可启动的 MainActivity。", 
    "三、构建检查：Debug APK 构建成功，可证明 Java 编译、资源编译和打包通过；但不能单凭此证明设备上的显示和触摸行为正确。", 
    "四、设备验证：Android 35 模拟器上安装与启动成功；ADB 根目录查看成功；ADB 模拟点击两次成功，界面 XML 分别出现 Hello, World! 与 こんにちは、世界！。", 
    "五、问题分析：起初缺少 AVD 系统镜像，无法运行；安装 Android 35 x86_64 镜像并创建 AVD 后解决。首次构建缺少 ANDROID_HOME，设置 SDK 路径后解决。", 
    "六、运行截图：下方依次为中文初始界面、ADB 点击后的英语界面、ADB 点击后的日语界面；原始 PNG 与界面层级 XML 保存在 evidence 目录。", 
    "七、质量评价：纯代码界面、三语交互、三面国旗、自定义图标与姓名学号标题已实现，构建及模拟器实测通过。", 
    "八、仓库说明：按本人选择使用 GitHub 而非题目要求的 Gitee。若教师严格按 Gitee 检查，应另建 Gitee 仓库并替换报告链接。"
];
for (var i = 0; i < analysis.Length; i++) Fill(paragraphs[50 + i], analysis[i]);
foreach (var index in Enumerable.Range(58, 7).Reverse()) paragraphs[index].Remove();

var gallery = new Table(new TableProperties(
    new TableWidth { Width = "8306", Type = TableWidthUnitValues.Dxa },
    new TableLayout { Type = TableLayoutValues.Fixed }));
gallery.Append(new TableGrid(new GridColumn { Width = "2768" }, new GridColumn { Width = "2769" }, new GridColumn { Width = "2769" }));
var row = new TableRow();
string[] shots = ["hello_zh.png", "hello_en.png", "hello_ja.png"];
string[] labels = ["图1 中文初始界面", "图2 ADB切换英语", "图3 ADB切换日语"];
for (int i = 0; i < shots.Length; i++)
{
    var path = Path.Combine(root, "evidence", shots[i]);
    var part = doc.MainDocumentPart.AddImagePart(ImagePartType.Png);
    using (var stream = File.OpenRead(path)) part.FeedData(stream);
    string relationId = doc.MainDocumentPart.GetIdOfPart(part);
    long cx = 185 * 914400L / 100;
    long cy = cx * 2400 / 1080;
    var props = new DW.DocProperties { Id = (uint)(10 + i), Name = shots[i], Description = labels[i] };
    var picture = new PIC.Picture(
        new PIC.NonVisualPictureProperties(
            new PIC.NonVisualDrawingProperties { Id = 0U, Name = shots[i] },
            new PIC.NonVisualPictureDrawingProperties()),
        new PIC.BlipFill(new A.Blip { Embed = relationId }, new A.Stretch(new A.FillRectangle())),
        new PIC.ShapeProperties(
            new A.Transform2D(new A.Offset { X = 0L, Y = 0L }, new A.Extents { Cx = cx, Cy = cy }),
            new A.PresetGeometry(new A.AdjustValueList()) { Preset = A.ShapeTypeValues.Rectangle }));
    var drawing = new Drawing(new DW.Inline(
        new DW.Extent { Cx = cx, Cy = cy },
        new DW.EffectExtent { LeftEdge = 0L, TopEdge = 0L, RightEdge = 0L, BottomEdge = 0L },
        props,
        new DW.NonVisualGraphicFrameDrawingProperties(new A.GraphicFrameLocks { NoChangeAspect = true }),
        new A.Graphic(new A.GraphicData(picture) { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" }))
        { DistanceFromTop = 0U, DistanceFromBottom = 0U, DistanceFromLeft = 0U, DistanceFromRight = 0U });
    var cell = new TableCell(
        new TableCellProperties(new TableCellWidth { Width = i == 0 ? "2768" : "2769", Type = TableWidthUnitValues.Dxa }),
        new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }), new Run(drawing)),
        new Paragraph(new ParagraphProperties(new Justification { Val = JustificationValues.Center }), new Run(new Text(labels[i]))));
    row.Append(cell);
}
gallery.Append(row);
body.InsertBefore(gallery, paragraphs[65]);
doc.MainDocumentPart.Document.Save();
Console.WriteLine(output);
