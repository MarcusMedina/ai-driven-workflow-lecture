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
- ✅ **Bilder konverteras till placeholder-text** `[ Plats för bild: xxx ]` i ljusgrå, italic

### Limitationer

- Mermaid-diagram konverteras inte (PowerPoint har inte stöd)
- Komplexa CSS-styles från MARP översätts till närmaste PowerPoint-ekvivalent
- Bilder konverteras till placeholder-text - lägg till bilder manuellt i PowerPoint
- Tables stöds inte ännu

### Bilder

Markdown-bilder (`![alt text](path/to/image.png)`) konverteras automatiskt till placeholder-text:

```markdown
![QR Code](slides/qrcode.png)
```

Blir i PowerPoint:
```
[ Plats för bild: QR Code ]
```

Formatet är ljusgrå, italic text så du ser var bilderna ska läggas in.

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
