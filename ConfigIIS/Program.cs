using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ConfigIIS
{
    class Program
    {
        static void Main(string[] args)
        {
												string nl = Environment.NewLine;
												string[] colorNames = Enum.GetNames(typeof(ConsoleColor));
            try
            {
																PrintLatticeChar("IIS");
																for (int i = 0; i < 15; i++)
																{
																				Console.WriteLine();
																}
																Console.WriteLine("Version");
																Console.ReadLine();
																for (int x = 0; x < colorNames.Length; x++)
																{
																		Console.Write("{0,2}: ", x);
																		Console.BackgroundColor = ConsoleColor.Black;
																		Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colorNames[x]);
																		Console.Write("This is foreground color {0}.", colorNames[x]);
																		Console.ResetColor();
																		Console.WriteLine();
																}
                string folderPath = "C:\\inetpub\\wwwroot\\WebApi";
                string applicationPath = "/WebApi";
                string applicationPoolName = "WebApiService";
																Console.ReadLine();
                if (!FolderSecurityHelper.ExistFolderRights(folderPath))
                {
                    FolderSecurityHelper.SetFolderRights(folderPath);
                }
                if (!IISControlHelper.ExistApplicationPool(applicationPoolName))
                {
                    IISControlHelper.CreateApplicationPool(applicationPoolName);
                }
                if (!IISControlHelper.ExistApplication(applicationPath))
                {
                    IISControlHelper.CreateApplication(applicationPath, folderPath, applicationPoolName);
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

								static int[,] GetLatticeArray(string s)
								{
												FontStyle style = FontStyle.Regular;
												string familyName = "Verdana";
												float emSize = 14;
												Font f = new Font(familyName, emSize, style);

												int width = 16;
												int height = 16;
												Bitmap bm = new Bitmap(width, height);

											 Graphics g =	Graphics.FromImage(bm);

												Brush b = new SolidBrush(Color.Black);
												PointF pf = new PointF(-3,-4);

												g.DrawString(s, f, b, pf);
												g.Flush();
												g.Dispose();

												//bm.Save("test.bmp");

												int[,] a = new int[16,16];
												for (int i = 0; i < bm.Width; i++)
												{
																for (int j = 0; j < bm.Height; j++)
																{
																				Color c = bm.GetPixel(j, i);
																				if (c.Name == "0")
																				{
																								a[j,i] = 0;
																				}
																				else
																				{
																								a[j,i] = 1;
																				}
																}
												}
												bm.Dispose();
												return a;
								}

								static void PrintLatticeChar(string s)
								{
												int x = Console.CursorLeft;
												int y = Console.CursorTop;
												foreach (char c in s.ToCharArray())
												{
																int[,] a = GetLatticeArray(c.ToString());
																int charWidth = a.GetLength(0);
																for (int i = 0; i < charWidth; i++)
																{
																				for (int j = 0; j < charWidth; j++)
																				{
																								if (a[j, i] == 1)
																								{
																												Console.Write("#");
																								}
																								else
																								{
																												Console.Write(" ");
																								}
																				}
																				x = Console.CursorLeft - charWidth;
																				if (x <= 0)
																				{
																								x = 1;
																				}
																				Console.CursorLeft = x;
																				Console.CursorTop++;
																}
																if((Console.CursorLeft + charWidth) > Console.WindowWidth){
																				Console.CursorTop += charWidth;
																}else{
																				Console.CursorLeft += charWidth;
																				Console.CursorTop -= charWidth;
																}
												}
								}
    }
}
