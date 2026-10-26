// .NET Framework 项目不会像 net6.0-windows 那样通过 UseWindowsForms 自动引入这些命名空间，
// 所以在这里用全局 using 提供 WinForms / Drawing 类型。
global using System.Windows.Forms;
global using System.Drawing;
