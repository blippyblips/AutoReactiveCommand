using Microsoft.CodeAnalysis;

namespace AutoReactiveCommandSourceGenerator;

public class CommandExtensionInfo
{
  private const string unitTypeName = "System.Reactive.Unit";

  public string MethodName { get; set; }
  public ITypeSymbol MethodReturnType { get; set; }
  public ITypeSymbol ArgumentType { get; set; }
  public bool IsTask { get; set; }
  public bool IsReturnTypeVoid { get; set; }

  public string GetOutputTypeText () => IsReturnTypeVoid ? unitTypeName : MethodReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

  public string GetInputTypeText () => ArgumentType == null ? unitTypeName : ArgumentType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
}