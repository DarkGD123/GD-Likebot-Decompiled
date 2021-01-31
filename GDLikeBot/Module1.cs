using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using GDLikeBot.My;
using Microsoft.VisualBasic.CompilerServices;

namespace GDLikeBot
{
	[StandardModule]
	internal sealed class Module1
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _Closure_0024__
		{
			public static readonly _Closure_0024__ _0024I;

			public static ParameterizedThreadStart _0024IR17_002D1;

			public static ParameterizedThreadStart _0024IR17_002D2;

			public static ParameterizedThreadStart _0024IR17_002D3;

			public static ParameterizedThreadStart _0024IR17_002D4;

			public static ParameterizedThreadStart _0024IR17_002D5;

			public static ParameterizedThreadStart _0024IR17_002D6;

			static _Closure_0024__()
			{
				_0024I = new _Closure_0024__();
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D1(object a0)
			{
				LikeItem((List<string>)a0);
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D2(object a0)
			{
				Rate((List<string>)a0);
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D3(object a0)
			{
				Download((List<string>)a0);
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D4(object a0)
			{
				DemonRating((List<string>)a0);
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D5(object a0)
			{
				LikeComment((List<string>)a0);
			}

			[DebuggerHidden]
			internal void _Lambda_0024__R17_002D6(object a0)
			{
				LikeProfile((List<string>)a0);
			}
		}

		private static Random Rnd = new Random();

		private static int itemID = 0;

		private static int LevelID = 0;

		private static int Stars = 0;

		private static int ThreadsCompleted = 0;

		private static WebProxy myProxy;

		private static int State = 1;

		private static int Demon = 0;

		private static int ProfileID = 0;

		private static List<string> proxies = new List<string>();

		private static bool CP = false;

		private static int MaxThreads = 256;

		private static int Counter = 0;

		private static int Succes = 0;

		[STAThread]
		public static void Main()
		{
			if (!MySettingsProperty.Settings.Subscribe)
			{
				Console.WriteLine("Love my hacks? Remember to subscribe!");
				Process.Start("https://www.youtube.com/MikeWe?sub_confirmation=1");
				Thread.Sleep(2500);
				MySettingsProperty.Settings.Subscribe = true;
				((ApplicationSettingsBase)MySettingsProperty.Settings).Save();
			}
			try
			{
				Console.set_ForegroundColor((ConsoleColor)14);
				Console.WriteLine("--------<GD Exploit King MikeWe>---------");
				Console.WriteLine("GDLikebot by MikeWe, Version 1.1.1");
				Console.WriteLine("-----------------------------------------");
				Console.Write("C-P mode (1/0):");
				CP = Conversions.ToBoolean(Console.ReadLine());
				Console.Write("MaxThreads (256 Recommended):");
				MaxThreads = Conversions.ToInteger(Console.ReadLine());
				Console.Write("LevelID:");
				LevelID = Conversions.ToInteger(Console.ReadLine());
				if (CP)
				{
					Console.Write("AccountID (OPTIONAL):");
					ProfileID = Conversions.ToInteger(Console.ReadLine());
					Console.Write("ItemID (Level/Profile Comments):");
					itemID = Conversions.ToInteger(Console.ReadLine());
				}
				else
				{
					Console.Write("StarRating (OPTIONAL)(Max 10):");
					Stars = Conversions.ToInteger(Console.ReadLine());
					Console.Write("DemonRating (OPTIONAL)(Max 5):");
					Demon = Conversions.ToInteger(Console.ReadLine());
				}
				Console.Write("Like/Dislike (1/0):");
				State = Conversions.ToInteger(Console.ReadLine());
				GrabProxies();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Console.WriteLine("You typed something wrong, restarting...");
				Thread.Sleep(2500);
				Console.Clear();
				Main();
				ProjectData.ClearProjectError();
			}
		}

		public static void GrabProxies()
		{
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Expected O, but got Unknown
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Expected O, but got Unknown
			//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Expected O, but got Unknown
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Expected O, but got Unknown
			List<string> list = new List<string>();
			string path = Directory.GetCurrentDirectory() + "\\Sources.txt";
			using (StreamReader streamReader = new StreamReader(path))
			{
				while (streamReader.Peek() != -1)
				{
					list.Add(streamReader.ReadLine());
				}
			}
			Console.WriteLine("Collecting Fresh Proxies...");
			IEnumerator enumerator2 = default(IEnumerator);
			foreach (string item in list)
			{
				try
				{
					HttpWebRequest val = (HttpWebRequest)WebRequest.Create(item);
					HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
					StreamReader streamReader2 = new StreamReader(val2.GetResponseStream());
					string text = streamReader2.ReadToEnd();
					Regex val3 = new Regex("[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}:[0-9]{1,4}");
					MatchCollection val4 = val3.Matches(text);
					try
					{
						enumerator2 = val4.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							Match val5 = (Match)enumerator2.Current;
							proxies.Add(((Capture)val5).ToString());
						}
					}
					finally
					{
						if (enumerator2 is IDisposable)
						{
							(enumerator2 as IDisposable).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ProjectData.ClearProjectError();
				}
			}
			Console.WriteLine("Collected " + Conversions.ToString(proxies.Count) + " Proxies!");
			Thread.Sleep(2500);
			SetProxies();
		}

		public static void SetProxies()
		{
			Console.WriteLine("LevelID: " + Conversions.ToString(LevelID) + ", Proxies loaded: " + Conversions.ToString(proxies.Count));
			Thread.Sleep(2500);
			Console.Clear();
			Console.WriteLine("Starting Bots...");
			int num = Math.Min(proxies.Count, MaxThreads);
			int num2 = num;
			checked
			{
				for (int i = 0; i <= num2; i++)
				{
					double num3 = (double)proxies.Count / (double)num * (double)i;
					List<string> range = proxies.GetRange((int)Math.Round(Math.Floor(num3)), (int)Math.Round(Math.Min(proxies.Count, num3 + (double)proxies.Count / (double)num) - num3));
					Thread thread = new Thread((_Closure_0024__._0024IR17_002D1 != null) ? _Closure_0024__._0024IR17_002D1 : (_Closure_0024__._0024IR17_002D1 = delegate(object a0)
					{
						LikeItem((List<string>)a0);
					}));
					Thread thread2 = new Thread((_Closure_0024__._0024IR17_002D2 != null) ? _Closure_0024__._0024IR17_002D2 : (_Closure_0024__._0024IR17_002D2 = delegate(object a0)
					{
						Rate((List<string>)a0);
					}));
					Thread thread3 = new Thread((_Closure_0024__._0024IR17_002D3 != null) ? _Closure_0024__._0024IR17_002D3 : (_Closure_0024__._0024IR17_002D3 = delegate(object a0)
					{
						Download((List<string>)a0);
					}));
					Thread thread4 = new Thread((_Closure_0024__._0024IR17_002D4 != null) ? _Closure_0024__._0024IR17_002D4 : (_Closure_0024__._0024IR17_002D4 = delegate(object a0)
					{
						DemonRating((List<string>)a0);
					}));
					Thread thread5 = new Thread((_Closure_0024__._0024IR17_002D5 != null) ? _Closure_0024__._0024IR17_002D5 : (_Closure_0024__._0024IR17_002D5 = delegate(object a0)
					{
						LikeComment((List<string>)a0);
					}));
					Thread thread6 = new Thread((_Closure_0024__._0024IR17_002D6 != null) ? _Closure_0024__._0024IR17_002D6 : (_Closure_0024__._0024IR17_002D6 = delegate(object a0)
					{
						LikeProfile((List<string>)a0);
					}));
					if (!CP)
					{
						thread3.Start(range);
						thread.Start(range);
						if (Stars != 0)
						{
							thread2.Start(range);
						}
						if (Demon != 0)
						{
							thread4.Start(range);
						}
					}
					else if (LevelID != 0)
					{
						thread5.Start(range);
					}
					else
					{
						thread6.Start(range);
					}
				}
				while (ThreadsCompleted < num)
				{
					Thread.Sleep(250);
					Console.Clear();
					Console.WriteLine("Total Requests [" + Conversions.ToString(Counter) + "]");
					Console.WriteLine("Total Failed [" + Conversions.ToString(Counter - Succes) + "]");
					Console.WriteLine("Total OK Requests [" + Conversions.ToString(Succes) + "]");
				}
				Console.Clear();
				Console.WriteLine("All proxies are used! (You can close the bot now!)");
			}
		}

		public static void LikeItem(List<string> ProxyList)
		{
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Expected O, but got Unknown
			//IL_0081: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Expected O, but got Unknown
			//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Expected O, but got Unknown
			checked
			{
				foreach (string Proxy in ProxyList)
				{
					try
					{
						string s = ((State != 1) ? ("itemID=" + Conversions.ToString(LevelID) + "&like=0&type=1&secret=Wmfd2893gb7") : ("itemID=" + Conversions.ToString(LevelID) + "&like=1&type=1&secret=Wmfd2893gb7"));
						myProxy = new WebProxy(Proxy);
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/likeGJItem21.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if (Operators.CompareString(text, "1", false) == 0)
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}

		public static void Rate(List<string> Proxylist)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Expected O, but got Unknown
			//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ea: Expected O, but got Unknown
			checked
			{
				foreach (string item in Proxylist)
				{
					try
					{
						myProxy = new WebProxy(item);
						string s = "gameVersion=21&binaryVersion=33&gdw=0&levelID=" + Conversions.ToString(LevelID) + "&stars=" + Conversions.ToString(Stars) + "&secret=Wmfd2893gb7";
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/rateGJStars20.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if (Operators.CompareString(text, "1", false) == 0)
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}

		public static void Download(List<string> Proxylist)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Expected O, but got Unknown
			//IL_00df: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Invalid comparison between Unknown and I4
			checked
			{
				foreach (string item in Proxylist)
				{
					try
					{
						myProxy = new WebProxy(item);
						string s = "gameVersion=21&binaryVersion=33&gdw=0&levelID=" + Conversions.ToString(LevelID) + "&inc=1&extras=0&secret=Wmfd2893gb7";
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/downloadGJLevel22.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if ((unchecked((int)val2.get_StatusCode()) != 400) & (Operators.CompareString(text, "-1", false) != 0))
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}

		public static void DemonRating(List<string> Proxylist)
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Expected O, but got Unknown
			//IL_0107: Unknown result type (might be due to invalid IL or missing references)
			//IL_010e: Expected O, but got Unknown
			checked
			{
				foreach (string item in Proxylist)
				{
					try
					{
						myProxy = new WebProxy(item);
						string s = "gameVersion=21&binaryVersion=33&gdw=0&accountID=" + Conversions.ToString(Rnd.Next(100000, 225757)) + "&gjp=R1hFQVNQRVBG&levelID=" + Conversions.ToString(LevelID) + "&rating=" + Conversions.ToString(Demon) + "&secret=Wmfp3879gc3";
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/rateGJDemon21.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if (Operators.CompareString(text, "1", false) == 0)
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}

		public static void LikeComment(List<string> ProxyList)
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			checked
			{
				foreach (string Proxy in ProxyList)
				{
					try
					{
						string s = ((State != 1) ? ("itemID=" + Conversions.ToString(itemID) + "&like=0&type=2&secret=Wmfd2893gb7&levelID=" + Conversions.ToString(LevelID)) : ("itemID=" + Conversions.ToString(itemID) + "&like=1&type=2&secret=Wmfd2893gb7&levelID=" + Conversions.ToString(LevelID)));
						myProxy = new WebProxy(Proxy);
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/likeGJItem21.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if (Operators.CompareString(text, "1", false) == 0)
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}

		public static void LikeProfile(List<string> ProxyList)
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_007c: Expected O, but got Unknown
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Expected O, but got Unknown
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			checked
			{
				foreach (string Proxy in ProxyList)
				{
					try
					{
						string s = ((State != 1) ? ("itemID=" + Conversions.ToString(itemID) + "&like=0&type=3&secret=Wmfd2893gb7&accountID=" + Conversions.ToString(ProfileID)) : ("itemID=" + Conversions.ToString(itemID) + "&like=1&type=3&secret=Wmfd2893gb7&accountID=" + Conversions.ToString(ProfileID)));
						myProxy = new WebProxy(Proxy);
						UTF8Encoding uTF8Encoding = new UTF8Encoding();
						byte[] bytes = uTF8Encoding.GetBytes(s);
						HttpWebRequest val = (HttpWebRequest)WebRequest.Create("http://www.boomlings.com/database/likeGJItem21.php");
						val.set_Proxy((IWebProxy)(object)myProxy);
						val.set_Method("POST");
						val.set_KeepAlive(true);
						val.set_ContentType("application/x-www-form-urlencoded");
						val.set_ContentLength(unchecked((long)bytes.Length));
						Stream requestStream = val.GetRequestStream();
						requestStream.Write(bytes, 0, bytes.Length);
						requestStream.Close();
						HttpWebResponse val2 = (HttpWebResponse)val.GetResponse();
						StreamReader streamReader = new StreamReader(val2.GetResponseStream());
						string text = streamReader.ReadToEnd();
						if (Operators.CompareString(text, "1", false) == 0)
						{
							Succes++;
						}
					}
					catch (Exception ex)
					{
						ProjectData.SetProjectError(ex);
						Exception ex2 = ex;
						ProjectData.ClearProjectError();
					}
					Counter++;
				}
				ThreadsCompleted++;
			}
		}
	}
}
