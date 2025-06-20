using System;
using System.Collections.Generic;
using System.Linq;
using System.util;
using System.Web;
using iTextSharp.text;
using System.IO;
using System.Web.Hosting;
using MotorSolutionNet.Models;
using MySqlX.XDevAPI;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Globalization;


namespace MotorSolutionNet.Services
{
    public class PdfServices
    {
        private readonly BillingService _billingService;
        private readonly RepairService _repairService;
        public PdfServices()
        {
            _billingService = new BillingService();
            _repairService = new RepairService();
        }
        private byte[] GenerarPdfDesdePlantilla(PdfBilling  pdf, List<RepairDetails> detalles)
        { // Obtener la ruta absoluta de la plantilla
            string plantillaPath = HostingEnvironment.MapPath("~/Resources/Plantilla.html");

            if (string.IsNullOrWhiteSpace(plantillaPath) || !File.Exists(plantillaPath))
                throw new FileNotFoundException($"❌ No se encontró la plantilla HTML en: {plantillaPath}");

            // Leer el contenido HTML de la plantilla
            string html = File.ReadAllText(plantillaPath);

            // Función local para evitar valores nulos o vacíos
            string Safe(string value) => string.IsNullOrWhiteSpace(value) ? "-" : value;

            // Reemplazos con control de errores individuales
            try { html = html.Replace("@COMPANYNAME", Safe(pdf.CompanyName)); } catch { throw new FormatException("Error en COMPANYNAME"); }
            try { html = html.Replace("@COMPANYADDRES", Safe(pdf.CompanyAddress)); } catch { throw new FormatException("Error en COMPANYADDRES"); }
            try { html = html.Replace("@COMPANYPHONE", Safe(pdf.CompanyPhone)); } catch { throw new FormatException("Error en COMPANYPHONE"); }
            try { html = html.Replace("@COMPANYEMAIL", Safe(pdf.CompanyEmail)); } catch { throw new FormatException("Error en COMPANYEMAIL"); }
            try { html = html.Replace("@NIT", Safe(pdf.Nit)); } catch { throw new FormatException("Error en NIT"); }
            try { html = html.Replace("@CLIENTENAME", Safe(pdf.ClientName)); } catch { throw new FormatException("Error en CLIENTENAME"); }
            try { html = html.Replace("@BILLINGID", pdf.BillingId.ToString()); } catch { throw new FormatException("Error en BILLINGID"); }
            try { html = html.Replace("@ENTRYDATE", Safe(pdf.EntryDate)); } catch { throw new FormatException("Error en ENTRYDATE"); }
            try { html = html.Replace("@DEPARTUREDATE", pdf.DepartureDate); } catch { throw new FormatException("Error en DEPAUREDATE"); }

            // Validación de fecha
            try
            {
                var fecha = DateTime.TryParse(pdf.BillingDate, out var parsedDate)
                    ? parsedDate.ToString("dd/MM/yyyy")
                    : "-";
                html = html.Replace("@BILLINGDATE", fecha);
            }
            catch { throw new FormatException("Error en BILLINGDATE"); }

            try { html = html.Replace("@MODEL", Safe(pdf.Model)); } catch { throw new FormatException("Error en MODEL"); }
            try { html = html.Replace("@PLATE", Safe(pdf.Plate)); } catch { throw new FormatException("Error en PLATE"); }

            // Formato de monto seguro (sin símbolo $, para evitar errores HTML)
            try
            {
                string montoFormateado = pdf.Amount.ToString("N0", CultureInfo.InvariantCulture);
                html = html.Replace("@AMOUNT", "$" + montoFormateado);
            }
            catch { throw new FormatException($"Error al formatear el monto: {pdf.Amount}"); }

            // Generar las filas del detalle
            string filas = string.Empty;

            foreach (var item in detalles)
            {
                try
                {
                    string precio = "$" + item.Price.ToString("N0", CultureInfo.InvariantCulture);

                    filas += "<tr>";
                    filas += $"<td>{Safe(item.RepairServices)}</td>";
                    filas += $"<td>{Safe(item.RepairDescription)}</td>";
                    filas += $"<td>{precio}</td>";
                    filas += "</tr>";
                }
                catch (Exception ex)
                {
                    throw new FormatException($"Error en detalle: {item.RepairServices}, Precio: {item.Price}", ex);
                }
            }

            html = html.Replace("@FILAS", filas);

            // Generar el PDF desde el HTML con XMLWorker
            using (MemoryStream ms = new MemoryStream())
             {
                 Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                 PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                 pdfDoc.Open();

                 using (StringReader sr = new StringReader(html))
                 {
                     XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                 }

                 pdfDoc.Close();
                 return ms.ToArray();
             }

            /* using (MemoryStream ms = new MemoryStream())
             {
                 Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                 PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                 pdfDoc.Open();

                 // Línea de prueba simple
                 pdfDoc.Add(new Paragraph("✅ Test PDF generado con éxito"));

                 pdfDoc.Close();
                 return ms.ToArray();
             }*/


        }

        public byte[] GenerarPdf(int billingId)
        {
            var pdf = _billingService.GetFullBilling(billingId);
            var repair = _repairService.GetRepairsDetails(pdf.RepairId);
            return GenerarPdfDesdePlantilla(pdf, repair);
        }
    }
}