import csv
import re
import os
import string

################################################################
# ALL POSSIBLE SOLUTIONS
################################################################

csvFile = './crystallonlevels.csv'
csvAnalysis = './analysis/crystallon_analysis.csv'
csvEvents = './crystallonevents.csv'

GoalDict = {}

analysisRows = []
analysisData = csv.DictReader(open(csvAnalysis, 'rb'))
for row in analysisData:
    level = row['Level']
    if row['Length'] != 'XX':
        goal = [row['Length'], row['Points']]
        if GoalDict.has_key(level):
            # check if this goal has been added already, and add if it not.
            try:
                GoalDict[level].index(goal)
            except ValueError:
                GoalDict[level].append(goal)
        else:
            GoalDict[level] = [goal]

################################################################
# EVENT DATA
################################################################		

EventDict = {}
eventRows = []
eventCsvData = csv.DictReader(open(csvEvents, 'rb'))
for row in eventCsvData:
	if row['Level'] != '':
		eventRows.append(row)
for row in eventRows:
	level = row['Level']
	if EventDict.has_key(level) == False:
		EventDict[level] = []
	EventDict[level].append(row)

################################################################
# LEVEL DATA
################################################################

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    keys = row.keys()
    for key in keys:
        if ( key == ''):
            del row[key]
    if row['Level'] != '':
        rows.append(row)

xmlFile = './levels/LevelData.xml'
xmlData = open(xmlFile, 'w')
xmlData.write("<GameData>\n")

for row in rows:
    xmlData.write('\t<Level Value="' + row['Level'] + '">\n')
    if row['Orientation'] == "DOES NOT":
        xmlData.write('\t\t<Orientation Value="0" />\n')
    xmlData.write('\t\t<Color LightHex="' + row['LIGHT'] + '" MidHex="' + row['MID']+ '" DarkHex="' + row['DARK'] + '" />\n')
    #xmlData.write('\t\t<Color Hex="' + row['RED'] + '" />\n')
    #xmlData.write('\t\t<Color Hex="' + row['BLUE'] + '" />\n')
    if row['Background'] != '':
		xmlData.write('\t\t<Background Main="' + row['MAIN'] + '" Grey="' + row['GREY'] + '" Black="' + row['BLACK'] + '" />\n')
    xmlData.write('\t\t<Pattern Path="Application/assets/images/' + row['Pattern'].lower() + '/gamePieces.png" />\n')
    xmlData.write('\t\t<Sound Prefix="stack' + row['Sound'][-1] + '" Glow="' + row['Glow'].lower() + '" />\n')
    for goal in GoalDict[row['Level']]:
        xmlData.write('\t\t<Goal Cubes="' + goal[0] + '" Score="' + goal[1] + '" />\n')
    #xmlData.write('\t\t<OldGoal Value="' + row['Goal'] + '" />\n')
    if int(row['Bonus']) > 0:
        xmlData.write('\t\t<Bonus Value="' + row['Bonus'] + '" />\n')
    if row['HitMeDisabled'] == 'TRUE':
		xmlData.write('\t\t<HitMeDisabled Value="true" />\n')
    if row['StandardPop'] != '':
        xmlData.write('\t\t<StandardPop Value="' + row['StandardPop'] + '" />\n')
    if row['SpawnRect'] != '':
		xmlData.write('\t\t<SpawnRect X="' + row['RECT_X'] + '" Y="' + row['RECT_Y'] + '" Width="' + row['RECT_W'] + '" Height="' + row['RECT_H'] + '" />\n')
    if row['Message'] != '':
        xmlData.write('\t\t<Message Title="' + row['TITLE'] + '" Body="' + row['BODY'] + '" />\n')
    if EventDict.has_key(row['Level']) :
		for event in EventDict[row['Level']]:
			xmlData.write('\t\t<Event Type="' + event['Type'] + '" Value="' + event['Value'] + '" Action="' + event['Action'] + '" Args="' + event['Args'] + '" />\n')
    xmlData.write('\t</Level>\n')

xmlData.write("</GameData>\n")
xmlData.close()
