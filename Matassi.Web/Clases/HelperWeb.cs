using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using Matassi.Dominio;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Web.Routing;

namespace Matassi.Web.Clases
{
	public static class HelperWeb
	{
		public static Image ScaleImage(byte[] imagen, int Width, int Height)
		{
			float sourceWidth;
			float sourceHeight;
			float destHeight = 0;
			float destWidth = 0;
			int sourceX = 0;
			int sourceY = 0;
			int destX = 0;
			int destY = 0;
			Image imgPhoto;

			if (imagen == null)
				return null;

			using (var ms = new MemoryStream(imagen))
			{
				imgPhoto = Image.FromStream(ms);
			}

			sourceWidth = imgPhoto.Width;
			sourceHeight = imgPhoto.Height;

			// force resize, might distort image
			if (Width != 0 && Height != 0)
			{
				destWidth = Width;
				destHeight = Height;
			}
			// change size proportially depending on width or height
			else if (Height != 0)
			{
				destWidth = (float)(Height * sourceWidth) / sourceHeight;
				destHeight = Height;
			}
			else
			{
				destWidth = Width;
				destHeight = (float)(sourceHeight * Width / sourceWidth);
			}

			Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
										PixelFormat.Format32bppPArgb);
			bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

			Graphics grPhoto = Graphics.FromImage(bmPhoto);
			grPhoto.CompositingQuality = CompositingQuality.Default;
			grPhoto.SmoothingMode = SmoothingMode.Default;
			grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

			grPhoto.DrawImage(imgPhoto,
				new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
				new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
				GraphicsUnit.Pixel);

			grPhoto.Dispose();

			return bmPhoto;
		}

		public static byte[] ImageToByte2(Image img)
		{
			if (img == null)
				return null;

			byte[] byteArray = new byte[0];
			using (MemoryStream stream = new MemoryStream())
			{
				img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
				stream.Close();

				byteArray = stream.ToArray();
			}
			return byteArray;
		}

		public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			var model = html.Encode(metadata.Model).Replace("\r\n", "<br />\r\n");

			if (String.IsNullOrEmpty(model))
				return MvcHtmlString.Empty;

			return MvcHtmlString.Create(model);
		}

		public static string DisplayWithBreaks(string texto)
		{
			return texto.Replace("\r\n", "<br />\r\n");
		}

		public static bool IsInteger(string input)
		{
			int test;
			return int.TryParse(input, out test);
		}

		public static DateTime? GetIfModifiedSinceUTCTime(HttpContext context)
		{
			DateTime? ifModifiedSinceTime = null;
			string ifModifiedSinceHeaderText = context.Request.Headers.Get("If-Modified-Since");

			if (!string.IsNullOrEmpty(ifModifiedSinceHeaderText))
			{
				ifModifiedSinceTime = DateTime.Parse(ifModifiedSinceHeaderText);
				//DateTime.Parse will return localized time but we want UTC
				ifModifiedSinceTime = ifModifiedSinceTime.Value.ToUniversalTime();
			}

			return ifModifiedSinceTime;
		}


		public class Mail
		{
			public static void SendMail(
					string mailFrom, string mailFromName,
					string replyTo, string replyToName,
					string mailTo, string mailToName,
					string subject, string text)
			{
				string mailUser = ConfigurationManager.AppSettings["smtpUser"];
				string mailPassword = ConfigurationManager.AppSettings["smtpPassword"];
				MailMessage message = new MailMessage(new MailAddress(mailUser, mailFromName), new MailAddress(mailTo, mailToName));

				message.IsBodyHtml = true;
				message.ReplyToList.Add(new MailAddress(replyTo, replyToName));
				message.Subject = subject;
				message.Body = text;
				//message.Bcc.Add(new MailAddress("gerenciapostventa@matassi.com.ar", "Matassi - Gerencia PostVenta"));


				SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["smtpServer"].ToString());

				smtp.Credentials = new NetworkCredential(mailUser, mailPassword);
				smtp.Send(message);

				/*message.f.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", ConfigurationSettings.AppSettings["smtpServer"].ToString());
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", mailUser); //set your username here
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", mailPassword);	//set your password here
				message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");	//basic authentication

				SmtpMail.SmtpServer = ConfigurationSettings.AppSettings["smtpServer"].ToString();//"sendmail4.brinkster.com"; 
				SmtpMail.Send(message);*/
			}
		}

		public class Parseo
		{
			/*public static IList<AutoahorroDato> ParseoOfertas(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				using (StreamReader streamReader = new StreamReader(input))
				{
					string archivo = streamReader.ReadToEnd();
					//string grupo, orden, modelo, observacion, concesionario;
					//float tAjustado, tLicitado;
					string pattern = @"Acto.+(?<Acto>\d{3}).+Fecha (?<Fecha>\d{2}/\d{2}/\d{2}).+Concesionario:(?<Concesionario>.+)\n.+Ofertas Recibidas (?<ofertasRecibidas>\d{2}/\d{2})";

					foreach (Match m in Regex.Matches(archivo, pattern))
					{
						archivoAutoahorro.Acto = int.Parse(m.Groups["Acto"].Value.Trim());
						archivoAutoahorro.Fecha = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), "dd/MM/yy", null);
						archivoAutoahorro.Concesionario = m.Groups["Concesionario"].Value.Trim();
						archivoAutoahorro.OfertasRecibidas = DateTime.ParseExact(m.Groups["ofertasRecibidas"].Value.Trim(), "MM/yy", null);
						break;
					}

					pattern = @"(?<Grupo>\d{4})-(?<Orden>\d{3})[ ]*(?<Modelo>[A-Za-z0-9]{5})[ ]*(?<TAjustado>[0-9]+\.[0-9]{2})[ ]*(?<TLicitado>[0-9]+\.[0-9]{2})(?<Observacion>.{10})[ ]*(?<Concesionario>\d{0,5})";
					int i = 0;
					IList<AutoahorroDato> res = new List<AutoahorroDato>();
					AutoahorroOferta oferta;

					foreach (Match m in Regex.Matches(archivo, pattern))
					{

						//grupo = orden = modelo = observacion = concesionario = string.Empty;
						//tAjustado = tLicitado = -1;

						//grupo = m.Groups["Grupo"].Value.Trim();
						//orden = m.Groups["Orden"].Value.Trim();
						//modelo = m.Groups["Modelo"].Value.Trim();
						//tAjustado = float.Parse(m.Groups["TAjustado"].Value, CultureInfo.InvariantCulture);
						//tLicitado = float.Parse(m.Groups["TLicitado"].Value, CultureInfo.InvariantCulture);
						//observacion = m.Groups["Observacion"].Value.Trim();
						//concesionario = m.Groups["Concesionario"].Value.Trim();

						//Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", grupo, orden, modelo, tAjustado, tLicitado, observacion, concesionario));

						oferta = new AutoahorroOferta();

						oferta.Grupo = m.Groups["Grupo"].Value.Trim();
						oferta.Orden = m.Groups["Orden"].Value.Trim();
						oferta.Modelo = m.Groups["Modelo"].Value.Trim();
						oferta.TAjustado = float.Parse(m.Groups["TAjustado"].Value, CultureInfo.InvariantCulture);
						oferta.TLicitado = float.Parse(m.Groups["TLicitado"].Value, CultureInfo.InvariantCulture);
						oferta.Observacion = m.Groups["Observacion"].Value.Trim();
						oferta.Concesionario = m.Groups["Concesionario"].Value.Trim();

						res.Add(oferta);

						i++;
					}
					//Console.Write(string.Format("Cantidad: {0}", i));

					return res;
				}
			}*/

			public static IList<AutoahorroDato> ParseoOfertas(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				if (archivoAutoahorro.NombreArchivo.ToLower().EndsWith("txt"))
					return ParseoOfertasTXT(input, ref archivoAutoahorro);
				else if (archivoAutoahorro.NombreArchivo.ToLower().EndsWith("xls") || archivoAutoahorro.NombreArchivo.ToLower().EndsWith("xlsx"))
					return ParseoOfertasXLS(input, ref archivoAutoahorro);
				return null;
			}

			public static IList<AutoahorroDato> ParseoOfertasXLS(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				HSSFWorkbook hssfwb;
				hssfwb = new HSSFWorkbook(input);

				ISheet sheet = hssfwb.GetSheetAt(0);
				if (sheet.LastRowNum > 3)
				{
					//ACTO N° 460 - REALIZADO EL 11-3-2016  CONCESIONARIO: AUTOTAG S.A.
					string pattern = @"ACTO.+(?<Acto>\d{3}).+REALIZADO EL (?<Fecha>\d{1,2}-\d{1,2}-\d{4}).+CONCESIONARIO:(?<Concesionario>.+).+";
					Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase);
					Match m = regEx.Match(sheet.GetRow(0).GetCell(0).StringCellValue);

					while (m.Success) 
					{
						archivoAutoahorro.Acto = int.Parse(m.Groups["Acto"].Value.Trim());
						archivoAutoahorro.Fecha = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), new string[] { "dd-M-yyyy", "dd-MM-yyyy", "d-M-yyyy", "d-MM-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
						archivoAutoahorro.Concesionario = m.Groups["Concesionario"].Value.Trim();

						m = m.NextMatch();
					}

					pattern = @".+ASAMBLEA (?<ofertasRecibidas>\d{1,2}/\d{4})";
					regEx = new Regex(pattern, RegexOptions.IgnoreCase);
					m = regEx.Match(sheet.GetRow(1).GetCell(0).StringCellValue);

					while (m.Success) 
					{
						archivoAutoahorro.OfertasRecibidas = DateTime.ParseExact(m.Groups["ofertasRecibidas"].Value.Trim(), new string [] {"MM/yyyy", "M/yyyy"}, CultureInfo.InvariantCulture, DateTimeStyles.None);

						m = m.NextMatch();
					}

					List<AutoahorroDato> ofertas = new List<AutoahorroDato>();
					AutoahorroOferta oferta = null;

					for (int row = 3; row <= sheet.LastRowNum; row++)
					{
						if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
						{
							
							if (Regex.IsMatch(sheet.GetRow(row).GetCell(0).StringCellValue, "^\\d+$"))
							{
								oferta = new AutoahorroOferta();

								oferta.SecNro			= (sheet.GetRow(row).GetCell(0) != null) ? sheet.GetRow(row).GetCell(0).StringCellValue.Trim() : string.Empty;
								oferta.Grupo			= (sheet.GetRow(row).GetCell(1) != null) ? sheet.GetRow(row).GetCell(1).StringCellValue.Trim() : string.Empty;
								oferta.Orden			= (sheet.GetRow(row).GetCell(2) != null) ? sheet.GetRow(row).GetCell(2).StringCellValue.Trim() : string.Empty;
								oferta.Modelo			= (sheet.GetRow(row).GetCell(3) != null) ? sheet.GetRow(row).GetCell(3).StringCellValue.Trim() : string.Empty;
								oferta.TAjustado		= (sheet.GetRow(row).GetCell(4) != null) ? float.Parse(sheet.GetRow(row).GetCell(4).StringCellValue.Trim(), CultureInfo.InvariantCulture) : 0;
								oferta.TLicitado		= (sheet.GetRow(row).GetCell(5) != null) ? float.Parse(sheet.GetRow(row).GetCell(5).StringCellValue.Trim(), CultureInfo.InvariantCulture) : 0;
								oferta.Observacion = (sheet.GetRow(row).GetCell(6) != null) ? (sheet.GetRow(row).GetCell(6).CellType == CellType.Numeric) ? sheet.GetRow(row).GetCell(6).NumericCellValue.ToString().Trim() : sheet.GetRow(row).GetCell(6).StringCellValue.Trim() : string.Empty;
								oferta.Concesionario	= (sheet.GetRow(row).GetCell(7) != null) ? sheet.GetRow(row).GetCell(7).StringCellValue.Trim() : string.Empty;

								ofertas.Add(oferta);
							}
						}
					}

					return ofertas;
				}

				return null;
			}

			public static IList<AutoahorroDato> ParseoOfertasTXT(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				using (StreamReader streamReader = new StreamReader(input))
				{
					string archivo = streamReader.ReadToEnd();
					string pattern = @"Acto.+(?<Acto>\d{3}).+Fecha (?<Fecha>\d{2}/\d{2}/\d{2}).+Concesionario:(?<Concesionario>.+)\n.+Ofertas Recibidas (?<ofertasRecibidas>\d{2}/\d{2})";

					foreach (Match m in Regex.Matches(archivo, pattern))
					{
						archivoAutoahorro.Acto = int.Parse(m.Groups["Acto"].Value.Trim());
						archivoAutoahorro.Fecha = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), "dd/MM/yy", null);
						archivoAutoahorro.Concesionario = m.Groups["Concesionario"].Value.Trim();
						archivoAutoahorro.OfertasRecibidas = DateTime.ParseExact(m.Groups["ofertasRecibidas"].Value.Trim(), "MM/yy", null);
						break;
					}

					string[] patterns = new string[3];
					patterns[0] = @"[ ]*(?<SECNRO>\d{1,4})[ ]*(?<GRUPO>\d{4})-(?<ORDEN>\d{3}).*";
					patterns[1] = @"[ ]*(?<MODELO>[A-Za-z0-9]{5})[ ]*(?<TAJUSTADO>[0-9]+\.[0-9]{2})[ ]*(?<TLICITADO>[0-9]+\.[0-9]{2})(?<OBSERVACION>.{10})[ ]*(?<CONCESIONARIO>\d{0,5})";

					int patternActual = 0;
					int i = 0;

					List<AutoahorroDato> res = new List<AutoahorroDato>();
					AutoahorroOferta oferta = null;
					string linea;
					bool flag1, flag2;
					flag1 = flag2 = false;

					streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
					while ((linea = streamReader.ReadLine()) != null)
					{
						Match matchPattern = Regex.Match(linea, patterns[patternActual]);

						if (patternActual == 0 && matchPattern.Success)
						{
							patternActual = 1;
							oferta = new AutoahorroOferta();

							oferta.SecNro = matchPattern.Groups["SECNRO"].Value.Trim();
							oferta.Grupo = matchPattern.Groups["GRUPO"].Value.Trim();
							oferta.Orden = matchPattern.Groups["ORDEN"].Value.Trim();
							flag1 = true;
						}
						else if (patternActual == 1 && matchPattern.Success)
						{
							patternActual = 2;
							if (oferta != null)
							{
								patternActual = 0;
								oferta.Modelo = matchPattern.Groups["MODELO"].Value.Trim();
								oferta.TAjustado = float.Parse(matchPattern.Groups["TAJUSTADO"].Value, CultureInfo.InvariantCulture);
								oferta.TLicitado = float.Parse(matchPattern.Groups["TLICITADO"].Value, CultureInfo.InvariantCulture);
								oferta.Observacion = matchPattern.Groups["OBSERVACION"].Value.Trim();
								oferta.Concesionario = matchPattern.Groups["CONCESIONARIO"].Value.Trim();
							}
							flag2 = true;
						}


						if (flag1 && flag2)
						{
							//Console.WriteLine(string.Format("gpo = {0}\nord = {1}\ndc = {2}\ndesv = {3}\ncuot = {4}\ndc2 = {5}\nplan = {6}\nmod = {7}\nvence = {8}\nnombre = {9}\nbanco = {10}\nsucursal = {11}\ncuenta = {12}\nalicuota = {13}\ncargos = {14}\nactalicuota = {15}\ncaactalic = {16}\nsegvida = {17}\nsegbien = {18}\nmora = {19}\ndebcred = {20}\nintliquid = {21}\notros = {22}\ntotal = {23}\n", gpo, ord, dc, desv, cuot, dc2, plan, mod, vence, nombre, banco, sucursal, cuenta, alicuota, cargos, actalicuota, caactalic, segvida, segbien, mora, debcred, intliquid, otros, total));
							res.Add(oferta);
							flag1 = flag2 = false;
							i++;
						}
					}

					return res;
				}
			}

			//public static IList<AutoahorroEmision> ParseoEmisiones(Stream input)
			public static List<AutoahorroDato> ParseoEmisiones(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				using (StreamReader streamReader = new StreamReader(input))
				{
					//string archivo = streamReader.ReadToEnd();
					//string grupo, orden, modelo, observacion, concesionario;
					//float tAjustado, tLicitado;

					bool fechaEnArchivo = false;
					string linea;
					string[] patterns = new string[3];
					patterns[0] = @"[ ]*(?<GPO>\d{4})[ ]*(?<ORD>\d{3})[ ]*(?<DC>\d{1})[ ]*(?<DESV>\d{2})[ ]*(?<CUOT>\d{2})[ ]*(?<DC2>\d{1})[ ]*(?<PLAN>\d{2})[ ]*(?<MOD>[A-Za-z0-9]{5})[ ]*(?<VENCE>[0-9]{2}/[0-9]{2}/[0-9]{4})[ ]*(?<NOMBRE>.{32})(?<BANCO>.{14})(?<SUCURSAL>.{19})(?<CUENTA>.+).*";
					patterns[1] = @"[ ]*(?<ALICUOTA>[0-9]+,[0-9]{2})[ ]*(?<CARGOS>[0-9]+,[0-9]{2})[ ]*(?<ACTALICUOTA>[0-9]+,[0-9]{2})[ ]*(?<CAACTALIC>[0-9]+,[0-9]{2})[ ]*(?<SEGVIDA>[0-9]+,[0-9]{2})[ ]*(?<SEGBIEN>[0-9]+,[0-9]{2})[ ]*(?<MORA>[0-9]+,[0-9]{2})[ ]*(?<DEBCRED>[-]?[0-9]+,[0-9]{2})[ ]*(?<INTLIQUID>[0-9]+,[0-9]{2})[ ]*(?<OTROS>[0-9]+,[0-9]{2})";
					patterns[2] = @".+(TOTAL :)[ ]+(?<TOTAL>([0-9]+\.)?[0-9]+,[0-9]{2}).*";

					int patternActual = 0;

					int i = 0;
					//IList<AutoahorroEmision> listaEmisiones = new List<AutoahorroEmision>();
					List<AutoahorroDato> listaEmisiones = new List<AutoahorroDato>();
					AutoahorroEmision emision = null;

					bool flag1, flag2, flag3;

					flag1 = flag2 = flag3 = false;

					while ((linea = streamReader.ReadLine()) != null)
					{
						Match matchPattern = Regex.Match(linea, patterns[patternActual]);

						if (patternActual == 0 && matchPattern.Success)
						{
							patternActual = 1;
							emision = new AutoahorroEmision();
							emision.Gpo = int.Parse(matchPattern.Groups["GPO"].Value);
							emision.Ord = int.Parse(matchPattern.Groups["ORD"].Value);
							emision.Dc = int.Parse(matchPattern.Groups["DC"].Value);
							emision.Desv = int.Parse(matchPattern.Groups["DESV"].Value);
							emision.Cuot = int.Parse(matchPattern.Groups["CUOT"].Value);
							emision.Dc2 = int.Parse(matchPattern.Groups["DC2"].Value);
							emision.Plan = matchPattern.Groups["PLAN"].Value;
							emision.Mod = matchPattern.Groups["MOD"].Value;
							emision.Vence = DateTime.ParseExact(matchPattern.Groups["VENCE"].Value, "dd/MM/yyyy", null);
							emision.Nombre = matchPattern.Groups["NOMBRE"].Value.Substring(0, 32).Trim();
							emision.Banco = matchPattern.Groups["BANCO"].Value.Substring(0, 14).Trim();
							emision.Sucursal = matchPattern.Groups["SUCURSAL"].Value.Substring(0, 19).Trim();
							emision.Cuenta = matchPattern.Groups["CUENTA"].Value;
							emision.Cuenta = emision.Cuenta.Substring(0, (emision.Cuenta.Length > 13) ? 13 : emision.Cuenta.Length).Trim();
							flag1 = true;
						}

						else if (patternActual == 1 && matchPattern.Success)
						{
							patternActual = 2;
							if (emision != null)
							{
								emision.Alicuota = float.Parse(matchPattern.Groups["ALICUOTA"].Value);
								emision.Cargos = float.Parse(matchPattern.Groups["CARGOS"].Value);
								emision.Actalicuota = float.Parse(matchPattern.Groups["ACTALICUOTA"].Value);
								emision.Caactalic = float.Parse(matchPattern.Groups["CAACTALIC"].Value);
								emision.SegVida = float.Parse(matchPattern.Groups["SEGVIDA"].Value);
								emision.SegBien = float.Parse(matchPattern.Groups["SEGBIEN"].Value);
								emision.Mora = float.Parse(matchPattern.Groups["MORA"].Value);
								emision.DebCred = float.Parse(matchPattern.Groups["DEBCRED"].Value);
								emision.Intliquid = float.Parse(matchPattern.Groups["INTLIQUID"].Value);
								emision.Otros = float.Parse(matchPattern.Groups["OTROS"].Value);
							}
							flag2 = true;
						}

						else if (patternActual == 2 && matchPattern.Success)
						{
							patternActual = 0;
							if (emision != null)
								emision.Total = float.Parse(matchPattern.Groups["TOTAL"].Value);
							flag3 = true;
						}

						if (flag1 && flag2 && flag3)
						{
							//Console.WriteLine(string.Format("gpo = {0}\nord = {1}\ndc = {2}\ndesv = {3}\ncuot = {4}\ndc2 = {5}\nplan = {6}\nmod = {7}\nvence = {8}\nnombre = {9}\nbanco = {10}\nsucursal = {11}\ncuenta = {12}\nalicuota = {13}\ncargos = {14}\nactalicuota = {15}\ncaactalic = {16}\nsegvida = {17}\nsegbien = {18}\nmora = {19}\ndebcred = {20}\nintliquid = {21}\notros = {22}\ntotal = {23}\n", gpo, ord, dc, desv, cuot, dc2, plan, mod, vence, nombre, banco, sucursal, cuenta, alicuota, cargos, actalicuota, caactalic, segvida, segbien, mora, debcred, intliquid, otros, total));
							listaEmisiones.Add(emision);
							flag1 = flag2 = flag3 = false;
							i++;

							if (!fechaEnArchivo)
							{
								archivoAutoahorro.Fecha = emision.Vence;
								archivoAutoahorro.Concesionario = "1411-03 - MATASSI E IMPERIALE S.A.";
								archivoAutoahorro.OfertasRecibidas = emision.Vence;
								fechaEnArchivo = true;
							}
						}
					}
					//Console.Write(string.Format("Cantidad: {0}", i));

					return listaEmisiones;
				}
			}

			public static IList<AutoahorroDato> ParseoGanadores(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				if (archivoAutoahorro.NombreArchivo.ToLower().EndsWith("txt"))
					return ParseoGanadoresTXT(input, ref archivoAutoahorro);
				else if (archivoAutoahorro.NombreArchivo.ToLower().EndsWith("xls") || archivoAutoahorro.NombreArchivo.ToLower().EndsWith("xlsx"))
					return ParseoGanadoresXLS(input, ref archivoAutoahorro);
				return null;
			}

			public static List<AutoahorroDato> ParseoGanadoresTXT(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				using (StreamReader streamReader = new StreamReader(input))
				{
					string archivo = streamReader.ReadToEnd();

					string pattern = @"Fecha (?<Fecha>\d{2}/\d{2}/\d{2})";
					foreach (Match m in Regex.Matches(archivo, pattern))
					{
						archivoAutoahorro.Fecha = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), "dd/MM/yy", null);
						archivoAutoahorro.Fecha = new DateTime(archivoAutoahorro.Fecha.Year, archivoAutoahorro.Fecha.Month, 20);
						archivoAutoahorro.OfertasRecibidas = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), "dd/MM/yy", null);
						archivoAutoahorro.FechaAlta = DateTime.Now;
						break;
					}


					pattern = @"(?<GRUPO>\d{1,4})-(?<ORDEN>\d{1,3})[ ]*(?<ADJUDICADO>.{38})(?<TIPOADJ>.{12})(?<IMPORTE>.{8})(?<GRILLA>.{10})(?<CONCESIONARIO>.{7})";

					int i = 0;


					List<AutoahorroDato> res = new List<AutoahorroDato>();
					AutoahorroGanador ganador;

					foreach (Match m in Regex.Matches(archivo, pattern))
					{
						ganador = new AutoahorroGanador();

						ganador.Grupo = int.Parse(m.Groups["GRUPO"].Value.Trim());
						ganador.Orden = int.Parse(m.Groups["ORDEN"].Value.Trim());
						ganador.Nombre = m.Groups["ADJUDICADO"].Value.Trim();
						ganador.Tipo = m.Groups["TIPOADJ"].Value.Trim();
						ganador.Monto = (m.Groups["IMPORTE"].Value.Trim() != string.Empty) ? float.Parse(m.Groups["IMPORTE"].Value, CultureInfo.InvariantCulture) : 0;
						ganador.Grilla = int.Parse(m.Groups["GRILLA"].Value.Trim());
						ganador.Concesionario = m.Groups["CONCESIONARIO"].Value.Trim();

						res.Add(ganador);

						i++;
					}

					return res;
				}
			}

			public static IList<AutoahorroDato> ParseoGanadoresXLS(Stream input, ref ArchivoAutoahorro archivoAutoahorro)
			{
				IWorkbook hssfwb = WorkbookFactory.Create(input);
				//HSSFWorkbook hssfwb = new HSSFWorkbook(input);

				ISheet sheet = hssfwb.GetSheetAt(0);
				if (sheet.LastRowNum > 3)
				{
					//ACTO N° 460 - REALIZADO EL 11-3-2016  CONCESIONARIO: AUTOTAG S.A.
					string pattern = @"ACTO.+(?<Acto>\d{3}).+REALIZADO EL (?<Fecha>\d{1,2}-\d{1,2}-\d{4}).+CONCESIONARIO:(?<Concesionario>.+).+";
					Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase);
					Match m = regEx.Match(sheet.GetRow(0).GetCell(0).StringCellValue);

					while (m.Success)
					{
						archivoAutoahorro.Acto = int.Parse(m.Groups["Acto"].Value.Trim());
						archivoAutoahorro.Fecha = DateTime.ParseExact(m.Groups["Fecha"].Value.Trim(), new string[] { "dd-M-yyyy", "dd-MM-yyyy", "d-M-yyyy", "d-MM-yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);
						archivoAutoahorro.Concesionario = m.Groups["Concesionario"].Value.Trim();

						m = m.NextMatch();
					}

					pattern = @".+ASAMBLEA (?<ofertasRecibidas>\d{1,2}/\d{4})";
					regEx = new Regex(pattern, RegexOptions.IgnoreCase);
					m = regEx.Match(sheet.GetRow(1).GetCell(0).StringCellValue);

					while (m.Success)
					{
						archivoAutoahorro.OfertasRecibidas = DateTime.ParseExact(m.Groups["ofertasRecibidas"].Value.Trim(), new string[] { "MM/yyyy", "M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None);

						m = m.NextMatch();
					}

					List<AutoahorroDato> ganadores = new List<AutoahorroDato>();
					AutoahorroGanador ganador = null;

					for (int row = 3; row <= sheet.LastRowNum; row++)
					{
						if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
						{

							if (Regex.IsMatch(sheet.GetRow(row).GetCell(0).StringCellValue.Trim(), "^\\d{4}\\-\\d{3}$"))
							{
								ganador = new AutoahorroGanador();

								ganador.Grupo = (sheet.GetRow(row).GetCell(0) != null) ? int.Parse(sheet.GetRow(row).GetCell(0).StringCellValue.Substring(0, 4)) : 0;
								ganador.Orden = (sheet.GetRow(row).GetCell(0) != null) ? int.Parse(sheet.GetRow(row).GetCell(0).StringCellValue.Substring(5, 3)) : 0;
								ganador.Tipo = (sheet.GetRow(row).GetCell(1) != null) ? sheet.GetRow(row).GetCell(1).StringCellValue : string.Empty;
								ganador.Monto = (sheet.GetRow(row).GetCell(2) != null) ? float.Parse(sheet.GetRow(row).GetCell(2).StringCellValue, CultureInfo.InvariantCulture) : 0;
								ganador.Grilla = (sheet.GetRow(row).GetCell(3) != null) ? int.Parse(sheet.GetRow(row).GetCell(3).StringCellValue) : 0;
								ganador.Concesionario = (sheet.GetRow(row).GetCell(4) != null) ? sheet.GetRow(row).GetCell(4).StringCellValue : string.Empty;

								ganadores.Add(ganador);
							}

							else if (Regex.IsMatch(sheet.GetRow(row).GetCell(0).StringCellValue.Trim(), "^\\d{4}$"))
							{
								ganador = new AutoahorroGanador();

								ganador.Grupo = (sheet.GetRow(row).GetCell(0) != null) ? int.Parse(sheet.GetRow(row).GetCell(0).StringCellValue.Trim()) : 0;
								ganador.Orden = (sheet.GetRow(row).GetCell(1) != null) ? int.Parse(sheet.GetRow(row).GetCell(1).StringCellValue.Trim()) : 0;
								ganador.Tipo = (sheet.GetRow(row).GetCell(2) != null) ? sheet.GetRow(row).GetCell(2).StringCellValue.Trim() : string.Empty;
								ganador.Monto = (sheet.GetRow(row).GetCell(3) != null) ? float.Parse(sheet.GetRow(row).GetCell(3).StringCellValue.Trim(), CultureInfo.InvariantCulture) : 0;
								ganador.Grilla = (sheet.GetRow(row).GetCell(4) != null) ? int.Parse(sheet.GetRow(row).GetCell(4).StringCellValue.ToString().Trim()) : 0;
								ganador.Concesionario = (sheet.GetRow(row).GetCell(5) != null) ? sheet.GetRow(row).GetCell(5).StringCellValue.ToString().Trim() : string.Empty;

								ganadores.Add(ganador);
							}
						}
					}

					return ganadores;
				}

				return null;
			}
		}

		public static void RegisterArea<T>(RouteCollection routes, object state) where T : AreaRegistration
		{
			AreaRegistration registration = (AreaRegistration)Activator.CreateInstance(typeof(T));
			AreaRegistrationContext context = new AreaRegistrationContext(registration.AreaName, routes, state);
			string tNamespace = registration.GetType().Namespace;
			if (tNamespace != null)
			{
				context.Namespaces.Add(tNamespace + ".*");
			}
			registration.RegisterArea(context);
		}
	}
}