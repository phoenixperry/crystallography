import csv
import re
import os
import string

####################################################################
# CARD DATA FOR EACH LEVEL
####################################################################

xmlFilePrefix = './levels/level_'
csvFile = './crystalloncards.csv'

# TEXT PATTERNS
pNote = re.compile(r'NOTE.*',re.IGNORECASE)

if not os.path.exists('./levels/'):
    os.makedirs('./levels/')

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    keys = row.keys()
    for key in keys:
        if ( re.match(pNote, key) != None or key == ''):
            del row[key]
    if row['Level'] != '':
        rows.append(row)

levelNum = 0
numCards = 0
numQualities = 0

#prepare the very first level description
xmlFile = xmlFilePrefix + str(levelNum) + ".xml"
xmlData = open(xmlFile, 'w')
xmlData.write("<Level>\n")

levelCounter = 0

for row in rows:
    if ( row['Level'] != str(levelNum) ): # end current level description and start another
        xmlData.write("</Level>")
        levelCounter += 1
        xmlData.close()
        levelNum = int(row['Level'])
        xmlFile = xmlFilePrefix + str(levelNum) + ".xml"
        xmlData = open(xmlFile, 'w')
        xmlData.write("<Level>\n")
    xmlData.write("\t<Card>\n")
    for entry in row: # add qualities to a card
        if ( entry != 'Level' and row[entry] != '' ):
            numQualities += 1
            xmlData.write('\t\t<Quality Name="Q' + entry + '" Value="' + str(row[entry][-1]) +'" />\n')
    xmlData.write("\t</Card>\n")
    numCards += 1

xmlData.write("</Level>") # finish the very last level description
levelCounter += 1
xmlData.close()

print "Level files generated: " + str(levelCounter + 1)
print "Cards generated: " + str(numCards)
print "Qualities generated: " + str(numQualities)
