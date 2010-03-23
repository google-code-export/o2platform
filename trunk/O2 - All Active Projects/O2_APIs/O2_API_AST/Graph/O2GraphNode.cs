using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using O2.Kernel.ExtensionMethods;
using System.Windows.Controls;
using System.Windows.Media;
using ICSharpCode.NRefactory.Ast;
//O2Ref:System.Core.dll
//O2Ref:PresentationCore.dll
//O2Ref:PresentationFramework.dll
//O2Ref:WindowsBase.dll

namespace O2.API.AST.Graph 
{
    public class O2GraphNode : Label
    {
        public CompilationUnit CompilationUnit { get; set; }
        public object OriginalObject { get; set; }
        public string NodeText { get; set; }
        public Action<O2GraphNode> onDoubleClick { get; set; }
        public Action<O2GraphNode> onMouseEnter { get; set; }
        public Action<O2GraphNode> onMouseLeave { get; set; }


        public O2GraphNode(object originalObject, CompilationUnit compilationUnit)
            : this(originalObject, originalObject.str(), compilationUnit)
        {            
        }

        public O2GraphNode(object originalObject, string nodeText, CompilationUnit compilationUnit)       
        {
            CompilationUnit = compilationUnit;
            OriginalObject = originalObject;
            NodeText = nodeText;
            this.Content = NodeText;
            setColorBasedOnObjectType();
            this.MouseDoubleClick+=(sender,e) => { if (onDoubleClick != null) onDoubleClick(this);};
            this.MouseEnter += (sender, e) => { if (onMouseEnter != null) onMouseEnter(this); };
            this.MouseLeave += (sender, e) => { if (onMouseLeave != null) onMouseLeave(this); };


        }

        /*public override string ToString()
        {
            return NodeText ?? "[null]";
        }*/
        public void setColorBasedOnObjectType()
        {
            switch (OriginalObject.typeName())
            { 
                case "ParameterDeclarationExpression":
                    Foreground = Brushes.Gray;
                    break;
                case "MethodDeclaration":
                case "MemberReferenceExpression":
                    Foreground = Brushes.Blue;
                    break;
                case "LocalVariableDeclaration":    
                case "VariableDeclaration":
                    Foreground = Brushes.DarkGreen;
                    break;                
                case "PrimitiveExpression":
                    Foreground = Brushes.Brown;
                    break;
                case "ReturnStatement":
                    Foreground = Brushes.Orange;
                    break;
                case "IdentifierExpression":
                    Foreground = Brushes.DarkViolet;
                    break;
                
                default:
                    Foreground = Brushes.Red;
                    break;
            }
        }

    }
}
