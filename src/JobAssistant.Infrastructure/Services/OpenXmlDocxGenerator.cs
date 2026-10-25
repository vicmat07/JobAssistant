using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using JobAssistant.Application.Abstractions;
using JobAssistant.Domain;
using JobAssistant.Domain.Models;
using JobAssistant.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace JobAssistant.Infrastructure.Services;

public sealed class OpenXmlDocxGenerator(IOptions<DocumentOptions> options) : IDocxGenerator
{
    public Task<string> GenerateResumeAsync(ApplicationDraft draft, Guid runId, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var path = CreatePath(runId, "resume");
        using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
        var body = Initialize(doc);
        Add(body, draft.ResumeHeadline, true, "32");
        Add(body, draft.ProfessionalSummary);
        Add(body, "Core Skills", true, "26");
        Add(body, string.Join(" • ", draft.CoreSkills));
        Add(body, "Professional Experience", true, "26");
        foreach (var exp in draft.Experience)
        {
            Add(body, $"{exp.Title} | {exp.Employer} | {exp.Dates}", true);
            foreach (var bullet in exp.Bullets) Add(body, "• " + bullet);
            if (exp.Technologies.Count > 0) Add(body, "Technologies: " + string.Join(", ", exp.Technologies));
        }
        if (draft.Education.Count > 0)
        {
            Add(body, "Education", true, "26");
            foreach (var item in draft.Education) Add(body, item);
        }
        if (draft.Certifications.Count > 0)
        {
            Add(body, "Certifications", true, "26");
            foreach (var item in draft.Certifications) Add(body, item);
        }
        doc.Save();
        return Task.FromResult(path);
    }

    public Task<string> GenerateCoverLetterAsync(ApplicationDraft draft, Guid runId, CancellationToken ct)
    {
        ct.ThrowIfCancellationRequested();
        var path = CreatePath(runId, "cover-letter");
        using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);
        var body = Initialize(doc);
        foreach (var paragraph in draft.CoverLetter.Split('\n', StringSplitOptions.RemoveEmptyEntries))
            Add(body, paragraph.Trim());
        doc.Save();
        return Task.FromResult(path);
    }

    private string CreatePath(Guid id, string suffix)
    {
        var dir = Path.GetFullPath(options.Value.OutputDirectory);
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, $"{id:N}-{suffix}.docx");
    }

    private static Body Initialize(WordprocessingDocument doc)
    {
        var main = doc.AddMainDocumentPart();
        main.Document = new Document(new Body());
        return main.Document.Body!;
    }

    private static void Add(Body body, string? text, bool bold = false, string size = "22")
    {
        var props = new RunProperties(new FontSize { Val = size });
        if (bold) props.Append(new Bold());
        var run = new Run(props, new Text(text ?? "") { Space = SpaceProcessingModeValues.Preserve });
        body.Append(new Paragraph(run));
    }
}