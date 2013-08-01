import csv
import re
import os
import string

################################################################
# ALL POSSIBLE SOLUTIONS
################################################################

csvFile = './crystallonlevels.csv'
csvAnalysis = './analysis/crystallon_analysis.csv'

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

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    keys = row.keys()
    for key in keys:
        if ( key == ''):
            del row[key]
    if row['Level'] != '':
        rows.append(row)


################################################################
# LEVEL DATA
################################################################

xmlFile = './levels/LevelData.xml'
xmlData = open(xmlFile, 'w')
xmlData.write("<GameData>\n")

for row in rows:
    xmlData.write('\t<Level Value="' + row['Level'] + '">\n')
    if row['Orientation'] == "DOES NOT":
        xmlData.write('\t\t<Orientation Value="0" />\n')
    xmlData.write('\t\t<Color Hex="' + row['PINK'] + '" />\n')
    xmlData.write('\t\t<Color Hex="' + row['RED'] + '" />\n')
    xmlData.write('\t\t<Color Hex="' + row['BLUE'] + '" />\n')
    xmlData.write('\t\t<Pattern Path="Application/assets/images/' + row['Pattern'].lower() + '/gamePieces.png" />\n')
    xmlData.write('\t\t<Sound Prefix="stack' + row['Sound'][-1] + '" Glow="' + row['Glow'].lower() + '" />\n')
    for goal in GoalDict[row['Level']]:
        xmlData.write('\t\t<Goal Cubes="' + goal[0] + '" Score="' + goal[1] + '" />\n')
    xmlData.write('\t\t<OldGoal Value="' + row['Goal'] + '" />\n')
    if int(row['Bonus']) > 0:
        xmlData.write('\t\t<Bonus Value="' + row['Bonus'] + '" />\n')
    if row['Message'] != '':
        xmlData.write('\t\t<Message Title="' + row['TITLE'] + '" Body="' + row['BODY'] + '" />\n')
    if row['StandardPop'] != '':
        xmlData.write('\t\t<StandardPop Value="' + row['StandardPop'] + '" />\n')
    xmlData.write('\t</Level>\n')

xmlData.write("</GameData>\n")
xmlData.close()
