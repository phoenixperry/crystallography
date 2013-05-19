import csv
import re
import os

#xmlFile = '../../../../data/game/Zombies_GameObjects_Library.xml'
xmlFilePrefix = './levels/level_'
csvFile = './crystallonlevels.csv'
##goalFile = 'myGoals.xml'

##pProgression = re.compile(r'PROGRESSION')
##pGameplay = re.compile(r'GAMEPLAY')
##pResource = re.compile(r'RESOURCE')
##pStore = re.compile(r'STORE')
##pMenu = re.compile(r'MENU')


##goalData = open(goalFile, 'w')


#p1 = re.compile(r'(.+)(^\t<group name="Goals".*?\t</group>)(.+</objects>)',re.MULTILINE+re.DOTALL)

##m = re.search(p1, xmlDataIn.read())

##m = xmlDataIn.read()

##xmlData.write(m.group(1))

if not os.path.exists('./levels/'):
    os.makedirs('./levels/')

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    # delete the "notes" column
    del row['']
    rows.append(row)

levelNum = 0
numCards = 0
numQualities = 0

#prepare the very first level description
xmlFile = xmlFilePrefix + str(levelNum) + ".xml"
xmlData = open(xmlFile, 'w')
xmlData.write("<Level>\n")


# the order they need to end up in
qualities = ['Sound', 'Orientation', 'Color', 'Pattern', 'Particle',
             'Animation']

for row in rows:
    if ( row['Level'] != str(levelNum) ): # end current level description and start another
        xmlData.write("</Level>")
        xmlData.close()
        levelNum = int(row['Level'])
        xmlFile = xmlFilePrefix + str(levelNum) + ".xml"
        xmlData = open(xmlFile, 'w')
        xmlData.write("<Level>\n")
    xmlData.write("\t<Card>\n")
    for quality in qualities:
        if quality == 'Particle' and row[quality] == '':
            row[quality] = "NONE 0"
        if row[quality] == '':
            continue
        numQualities += 1
        xmlData.write('\t\t<Quality Name="Q' + quality + '" Value="' + str(row[quality][-1]) + '" />\n')
    xmlData.write("\t</Card>\n")
    numCards += 1

xmlData.write("</Level>") # finish the very last level description
xmlData.close()

print "Level files generated: " + str(levelNum + 1)
print "Cards generated: " + str(numCards)
print "Qualities generated: " + str(numQualities)
