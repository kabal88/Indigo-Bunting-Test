using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeGenerator;
using Identifier;
using UnityEditor;
using UnityEngine;
    public class GenerateIdentifiersMap : Editor
    {
        private static string _scriptPath = Application.dataPath + "/Scripts/";
        private static readonly string _customGenerated = "/CustomGenerated/";
        
        [MenuItem("Options/Generate Identifiers Map")]
        public static void GenerateIdentifiers()
        {
            var identifiersContainers = AssetDatabase.FindAssets("t:IdentifierContainer")
              .Select(AssetDatabase.GUIDToAssetPath)
              .Select(AssetDatabase.LoadAssetAtPath<IdentifierContainer>).ToList();
            

            var sort = new Dictionary<Type, HashSet<IdentifierContainer>>(64);

            foreach (var identifier in identifiersContainers)
                AddToDictionary(sort, identifier);
            
            string maps = string.Empty;

            maps += new UsingSyntax("System.Collections.Generic", 1).ToString();

            foreach (var sorted in sort)
                maps += GetIdentifiersMap(sorted.Key, sorted.Value);
            
            var names = identifiersContainers.Select(x => x.name).ToHashSet();
            
            maps += GetIdentifierStringMap(names);

            SaveToFile(maps);
        }

        private static void AddToDictionary(Dictionary<Type, HashSet<IdentifierContainer>> dict, IdentifierContainer container)
        {
            var type = container.GetType();

            if (dict.ContainsKey(type))
                dict[type].Add(container);
            else
            {
                dict.Add(type, new HashSet<IdentifierContainer>());
                dict[type].Add(container);
            }
        }

        private static string GetIdentifierStringMap(HashSet<string> identifierNames)
        {
            var stringIdentifiersMap = new TreeSyntaxNode();
            var body = new TreeSyntaxNode();
            var dictionaryIntToString = new TreeSyntaxNode();

            stringIdentifiersMap.Add(new SimpleSyntax($"public static class IdentifierToStringMap" + CParse.Paragraph));

            stringIdentifiersMap.Add(new LeftScopeSyntax());
            stringIdentifiersMap.Add(body);
            stringIdentifiersMap.Add(new RightScopeSyntax());

            //dictionary
            body.Add(new TabSimpleSyntax(1, "public static readonly Dictionary<int, string> IntToString = new Dictionary<int, string>"));
            body.Add(new LeftScopeSyntax(1));
            body.Add(dictionaryIntToString);
            body.Add(new RightScopeSyntax(1, true));

            foreach (var identifier in identifierNames)
            {
                var name = identifier.Replace("Container", "");
                body.Add(new TabSimpleSyntax(1, $"public const string {name} = {CParse.Quote}{name}{CParse.Quote};"));
                dictionaryIntToString.Add(new TabSimpleSyntax(2, $"{CParse.LeftScope} {IndexGenerator.GetIndexForType(name)}, {CParse.Quote}{name}{CParse.Quote}{CParse.RightScope},"));
            }

            return stringIdentifiersMap.ToString();
        }

        private static string GetIdentifiersMap(Type type, HashSet<IdentifierContainer> identifierContainers)
        {
            var composeIdentifiersMap = new TreeSyntaxNode();
            var body = new TreeSyntaxNode();

            composeIdentifiersMap.Add(new SimpleSyntax($"public static class {type.Name}Map" + CParse.Paragraph));

            composeIdentifiersMap.Add(new LeftScopeSyntax());
            composeIdentifiersMap.Add(body);
            composeIdentifiersMap.Add(new RightScopeSyntax());

            foreach (var identifier in identifierContainers)
            {
                var name = identifier.name.Replace("Container", "");
                body.Add(new TabSimpleSyntax(1, $"public static int {name} => {identifier.Id};"));
            }

            return composeIdentifiersMap.ToString();
        }

        private static void SaveToFile(string data)
        {
            var find = Directory.GetFiles(Application.dataPath, "IdentifiersMaps.cs", SearchOption.AllDirectories);

            var pathToDirectory = _scriptPath + _customGenerated;
            var path = pathToDirectory + "IdentifiersMaps.cs";

            if (find != null && find.Length > 0 && !string.IsNullOrEmpty(find[0]))
            {
                path = find[0];
            }

            try
            {
                if (!Directory.Exists(pathToDirectory))
                    Directory.CreateDirectory(pathToDirectory);

                File.WriteAllText(path, data);
                var sourceFile2 = path.Replace(Application.dataPath, "Assets");
                AssetDatabase.ImportAsset(sourceFile2);
            }
            catch
            {
                Debug.LogError("can't make" + pathToDirectory);
            }
        }
    }

