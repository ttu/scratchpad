import sys
import os
import codecs

file = "citydata.csv"
outputFile = "citydata.out"
output = ""

def main():
	output = "public List<Tuple<string,string,int>> distances = new List<Tuple<string,string,int>>()\n\r{\n\r"

	with codecs.open(file, 'r', 'utf-8') as f:
		content = f.readlines()
		for line in content:
			items = line.strip().split(",")
			output += "\tnew Tuple<string,string,int>(\"{0}\",\"{1}\",{2}),\n\r".format(items[0],items[1],items[2])
	output = output[:-3] + "\n\r};"
	print(output)

	with codecs.open(outputFile, 'w', 'utf-8') as f:
		f.write(output)

if __name__=="__main__":
	main()