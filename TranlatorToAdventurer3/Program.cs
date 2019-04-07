﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Take4.Translator;
using Take4.Rs274ngcParser;

namespace TranlatorToAdventurer3 {
	/// <summary>
	/// Adventurer3用のファイルに修正する
	/// </summary>
	class Program {
		static void Main(string[] args) {
			string parameterFile = null;
			string targetGcodeFile = null;
			string outputFile = null;

			// パラメータの解析
			foreach(var arg in args) {
				if (arg.EndsWith(".json")) {
					if (File.Exists(arg)) {
						parameterFile = arg;
					}
				}
				else if (arg.EndsWith(".gx") || arg.EndsWith(".g")) {
					if (File.Exists(arg)) {
						targetGcodeFile = arg;
					}
				}
			}

			if (!string.IsNullOrEmpty(parameterFile) && !string.IsNullOrEmpty(targetGcodeFile)) {
				outputFile = Path.ChangeExtension(targetGcodeFile, ".gout");
				using (var gfile = File.Open(targetGcodeFile, FileMode.Open)) {
					// ファイルの種別取り出し
					var buffer = new List<Byte>();
					for (var val = gfile.ReadByte(); val != -1; val = gfile.ReadByte()) {
						buffer.Add((Byte)val);
						if (val == 0x0a) {
							break;
						}
					}
					var firstLine = Encoding.ASCII.GetString(buffer.ToArray());
					gfile.Seek(0, SeekOrigin.Begin);

					ToAdventurer3 toAdventurer3Process = null;
					using (var parameter = System.IO.File.OpenRead(parameterFile)) {
						var settings = new System.Runtime.Serialization.Json.DataContractJsonSerializerSettings();
						var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ToAdventurer3), settings);
						toAdventurer3Process = (ToAdventurer3)serializer.ReadObject(parameter);
					}

					if (firstLine.StartsWith("; generated by Slic3r 1.3.0")) {
						var tempFile = Path.GetTempFileName();
						Slic3rToBase toSlic3rProcess = null;
						using (var parameter = System.IO.File.OpenRead(parameterFile)) {
							var settings = new System.Runtime.Serialization.Json.DataContractJsonSerializerSettings();
							var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Slic3rToBase), settings);
							toSlic3rProcess = (Slic3rToBase)serializer.ReadObject(parameter);
						}
						if (toSlic3rProcess != null) {
							using (var temp = File.Open(tempFile, FileMode.Open)) {
								if (!toSlic3rProcess.Modify(gfile, temp)) {
									Console.WriteLine("Can't translate Slic3r file.");
									return;
								}
								temp.Seek(0, SeekOrigin.Begin);
								using (var output = File.Open(outputFile, FileMode.CreateNew)) {
									if (!toAdventurer3Process.Modify(temp, output)) {
										Console.WriteLine("Can't translate to Adventurer3 file.");
										return;
									}
								}
							}
						}
						File.Delete(tempFile);
						Console.WriteLine("Translate end.");
					}
					else if (firstLine.StartsWith("xgcode 1.0") || firstLine.StartsWith("; G-Code generated by Simplify3D(R) Version 4.1")) {
						using (var output = File.Open(outputFile, FileMode.CreateNew)) {
							if (!toAdventurer3Process.Modify(gfile, output)) {
								Console.WriteLine("Can't translate to Adventurer3 file.");
								return;
							}
						}
						Console.WriteLine("Translate end.");
					}
				}
			}
			else {
				Console.WriteLine("Can't translate.");
			}
		}
	}
}
