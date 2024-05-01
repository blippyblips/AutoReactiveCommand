using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AutoReactiveCommandSourceGenerator;

public static class Utils
{
  public static bool IsVoidFunction (this MethodDeclarationSyntax ms) => ms.ReturnType is PredefinedTypeSyntax predefined && predefined.Keyword.IsKind(SyntaxKind.VoidKeyword);

  public static bool CouldBeMethodWithAttributeInPartialClass (SyntaxNode node, CancellationToken ct, HashSet<string> distinctClasses) {
    if(node is MethodDeclarationSyntax method) {
      if(node.Parent is ClassDeclarationSyntax classDeclaration) {
         if(distinctClasses.Add(classDeclaration.Identifier.Text)) {
          return true;
        }
      }
    }
    return false;
  }
  public static bool IsPartial (ClassDeclarationSyntax classDeclaration) => classDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword));

  public static bool IsVoid (ITypeSymbol returnType) => returnType.SpecialType == SpecialType.System_Void;

  public static bool IsTask (ITypeSymbol returnType) => returnType.ToString() == "System.Threading.Tasks.Task" || returnType.ToString().StartsWith("System.Threading.Tasks.Task<");

  public static ITypeSymbol GetReturnType (ITypeSymbol ReturnType) {
    if(ReturnType is INamedTypeSymbol namedType && namedType.IsGenericType) {
      return namedType.TypeArguments[0];
    }
    return ReturnType;
  }

  public static bool IsTaskReturnType (ITypeSymbol typeSymbol) {
    var nameFormat = SymbolDisplayFormat.FullyQualifiedFormat;
    do {
      var typeName = typeSymbol?.ToDisplayString(nameFormat);
      if (typeName == "global::System.Threading.Tasks.Task")
        return true;

      typeSymbol = typeSymbol?.BaseType;
    } while (typeSymbol != null);

    return false;
  }
}
