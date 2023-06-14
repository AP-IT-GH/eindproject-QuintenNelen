# Cursed Tennis

## Inleiding

In dit spel is het de bedoeling dat je een spelletje tennis speelt tegen een AI-agent die speciaal getraind is om te winnen tegen zijn tegenstander. Het spel wordt gespeeld in Virtual Reality en volgt uiteraard de echte regels van tennis.

### Speltitel

De naam van het spel is "Cursed Tennis". In dit spel sta je namelijk tegenover een vervloekt tennisracket.

### Gebruikte methoden

Voor dit project maken we gebruik van Anaconda om de AI-agents te trainen. Voor het ontwikkelen van de omgeving gebruiken we Unity. Daarnaast maken we gebruik van TensorBoard om de trainingsresultaten op een overzichtelijke manier weer te geven en een goed overzicht te krijgen van hoe de agent traint.

## Tutorial

Hieronder vind je een uitgebreide tutorial waarin stap voor stap wordt uitgelegd hoe je de code van het spel kunt implementeren, de spelomgeving kunt opzetten en de AI-agent kunt trainen. Na het doorlopen van deze tutorial ben je vertrouwd met het werken met ML-agents, Unity en VR. Je zult in staat zijn om je eigen tenniservaring in VR te creëren en te verbeteren.

### Installatie

Hieronder vind je een oplijsting van welke versies van de software we gebruiken:

- Unity: versie 2021.3
- Anaconda (Python): versie 3.9.13

De installaties van deze zaken vind je allemaal goed gedocumenteerd online.

### Verloop spel

Je begint het spel in het hoofdmenu waar je, in VR uiteraard, enkele opties krijgt zoals het selecteren van het aantal sets. Als je het spel start kom je terecht in een stadion waar het spelletje tennis tegen de AI direct begint. De opslager van de bal wordt willekeurig gekozen. Je zal het spel ook kunnen pauzeren. Nadat alle sets gespeeld zijn, zal de winnaar bekend zijn en zal het spel eindigen. Hierna kom je terug uit op het hoofdmenu.

### Beschrijving code

Voor het project te realiseren maken we gebruik van verschillende scriptjes. Hieronder een korte uitleg wat elk script doet:

- GroundContact.cs: wordt gebruikt op de bal om contact met de grond te detecteren.
- HitWall.cs: dit script wordt gebruikt om te detecteren of de bal buiten gaat en voor het bepalen van de rewards die de agent krijgt in het algemeen.
- TennisAgent.cs: zorgt ervoor dat de tennis agent correct werkt en dat de puntentelling op de juiste manier wordt weergegeven.
- TennisArea.cs: script om de hele scene deftig op te zetten. Zorgt voor het resetten van de bal onder andere.
- TennisMatchManager.cs: dit script staat in voor het goed verlopen van een tennismatch, met alle correcte regels.

 Alle scripts zijn te vinden in de repo in de map "Assets/Tennis/scripts"

## Resultaten

Voor de agent in orde te krijgen hebben we hem uiteraard veel moeten trainen. Hieronder zal je enkele trainingsresultaten vinden en enkele observaties.

### Trainingsresultaten

### Observaties

De agent krijgt als observaties de positie van de bal mee, zijn eigen positie en positie naar waar de bal moet geschoten worden. Als de bal bij de agent is zal deze de bal terug slaan.

De agent krijgt een reward als die de bal heeft geraakt of een punt heeft gescoord. De agent krijgt meer reward als hij heeft gescoord dan wanneer hij een bal heeft geraakt.

## Conclusie

### Kort overzicht project

Zoals hierboven vermeld hebben we een spel gemaakt waarin je een potje tennis speelt tegen een getrainde AI agent. Uit de trainingsresultaten kunnen we concluderen dat we de agent zeer lang hebben moeten laten trainen voordat hij deftig kon tennissen. Ook zien we betere resultaten als naargelang er meer observations toegevoegd worden.

### Ervaringen

We hebben dit project goed ervaren en hebben er veel uit geleerd. Het trainen van de agent was soms wel een pijnlijk gegeven maar dit is uiteindelijk wel goed gekomen.

### Lessons learned/verbeteringen

- De agent lang genoeg laten trainen.
- Altijd controleren of er wel met de collision boxes ge-collide kan worden.
- Voldoende observations toevoegen.

## Bronvermelding

Unity-Technologies. (2023).  *GitHub - Unity-Technologies/ml-agents at fix-batch-tennis* . GitHub. https://github.com/Unity-Technologies/ml-agents/tree/fix-batch-tennis
