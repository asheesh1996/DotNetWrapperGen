﻿using DotNetWrapperGen.CodeModel;
using DotNetWrapperGen.CodeStructure;
using System.Linq;

namespace DotNetWrapperGen.Transformer
{
    public class BulletTransformer
    {
        private const string DefaultNamespace = "BulletSharp";

        public static void Transform(RootFolderDefinition rootFolder, NamespaceDefinition globalNamespace)
        {
            RenameHeaders(rootFolder);
            MoveGlobalNodes(globalNamespace);
        }

        private static void RenameHeaders(SourceItemDefinition item)
        {
            if (item.Name.StartsWith("bt"))
            {
                item.Name = item.Name.Substring(2);
            }

            foreach (var child in item.Children)
            {
                RenameHeaders(child);
            }
        }

        private static void MoveGlobalNodes(NamespaceDefinition globalNamespace)
        {
            NamespaceDefinition defaultNamespace = null;

            var globalNodes = globalNamespace.Children.ToList();
            foreach (var node in globalNodes)
            {
                if (node is NamespaceDefinition)
                {
                    continue;
                }

                if (defaultNamespace == null)
                {
                    globalNamespace
                        .Namespaces
                        .FirstOrDefault(ns => string.Equals(ns.Name, DefaultNamespace));
                    if (defaultNamespace == null)
                    {
                        defaultNamespace = new NamespaceDefinition(DefaultNamespace);
                        globalNamespace.AddChild(defaultNamespace);
                    }
                }

                defaultNamespace.AddChild(node);
            }
        }
    }
}