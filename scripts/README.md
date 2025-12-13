# Scripts

## md_to_pptx.py

Konverterar MARP Markdown-presentationer till PowerPoint (PPTX).

### Installation

```bash
pip install -r requirements.txt
```

### Användning

```bash
# Basic - skapar lecture_60min.pptx
python scripts/md_to_pptx.py slides/lecture_60min.md

# Med custom output-namn
python scripts/md_to_pptx.py slides/lecture_60min.md output/min_presentation.pptx
```

### Features

- ✅ Konverterar MARP markdown till PowerPoint
- ✅ Hanterar dark theme (extraherar från MARP frontmatter)
- ✅ Respekterar slide-separatorer (`---`)
- ✅ Formaterar headers (H1, H2, H3)
- ✅ Hanterar code blocks med syntax highlighting-färger
- ✅ Bullet points och numrerade listor
- ✅ Bold, italic, inline code
- ✅ Title slides (med `<!-- _class: lead -->`)
- ✅ 16:9 aspect ratio

### Limitationer

- Mermaid-diagram konverteras inte (PowerPoint har inte stöd)
- Komplexa CSS-styles från MARP översätts till närmaste PowerPoint-ekvivalent
- Bilder måste läggas till manuellt i PowerPoint efter konvertering
- Tables stöds inte ännu

### Exempel

```bash
cd /mnt/c/git/ailecture
python scripts/md_to_pptx.py slides/lecture_60min.md
```

Detta skapar `slides/lecture_60min.pptx` med:
- Dark theme (#1a1a1a bakgrund, #e1e1e1 text)
- Blå headers (#4a9eff för H1, #64b5f6 för H2)
- Grön kod (#a8e063 på #2d2d2d bakgrund)
- Alla slides från markdown-filen
