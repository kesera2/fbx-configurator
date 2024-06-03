using System.Reflection;
using System.Runtime.CompilerServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle("Test Target")]
[assembly: AssemblyDescription("")]
//#if DEBUG
[assembly: InternalsVisibleTo("FBXOptionsManager.Tests")]
//#endif
