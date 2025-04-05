namespace stellarCinema.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

public class PdfGenerator
{
    public static void GenerateTicket(string filmTitle, DateTime date, decimal price, string[] seats, string filePath)
    {
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(50);
                page.Size(PageSizes.A5);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(14));

                page.Header()
                    .Text("Bilet do Kina")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .Column(col =>
                    {
                        col.Item().Text($"🎬 Film: {filmTitle}");
                        col.Item().Text($"📅 Data i godzina: {date:dd-MM-yyyy HH:mm}");
                        col.Item().Text($"💰 Cena: {price} PLN");
                        col.Item().Text($"🎟️ Miejsca: {string.Join(", ", seats)}");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text("Dziękujemy za zakup! Miłego seansu 🎥");
            });
        })
        .GeneratePdf(filePath);
    }
}

