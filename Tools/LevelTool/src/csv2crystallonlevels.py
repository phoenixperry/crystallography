import csv
import re
import os
import string

#xmlFile = '../../../../data/game/Zombies_GameObjects_Library.xml'
xmlFilePrefix = './levels/level_'
csvFile = './crystallonlevels.csv'
##goalFile = 'myGoals.xml'

##pProgression = re.compile(r'PROGRESSION')
##pGameplay = re.compile(r'GAMEPLAY')
##pResource = re.compile(r'RESOURCE')
##pStore = re.compile(r'STORE')
##pMenu = re.compile(r'MENU')

pNote = re.compile(r'NOTE.*',re.IGNORECASE)


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
    #del row
    keys = row.keys()
    for key in keys:
        if ( re.match(pNote, key) != None or key == ''):
            #print key
            del row[key]
    if row['Level'] != '':
        rows.append(row)
        #print row

levelNum = 0
numCards = 0
numQualities = 0

#prepare the very first level description
xmlFile = xmlFilePrefix + str(levelNum) + ".xml"
xmlData = open(xmlFile, 'w')
xmlData.write("<Level>\n")

## The order is arbitrary, so there is no need to make this a hard-define.
# the order they need to end up in
#qualities = ['Sound', 'Orientation', 'Color', 'Pattern', 'Particle',
#             'Animation']

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
#    for quality in qualities:
#        if quality == 'Particle' and row[quality] == '':
#            row[quality] = "NONE 0"
#        if row[quality] == '':
#            continue
#		numQualities += 1
#        xmlData.write('\t\t<Quality Name="Q' + quality + '" Value="' + str(row[quality][-1]) + '" />\n')
    xmlData.write("\t</Card>\n")
    numCards += 1

xmlData.write("</Level>") # finish the very last level description
levelCounter += 1
xmlData.close()

print "Level files generated: " + str(levelCounter + 1)
print "Cards generated: " + str(numCards)
print "Qualities generated: " + str(numQualities)
