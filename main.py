# -*- coding: utf-8 -*-

import sys,os
parent_folder_path = os.path.abspath(os.path.dirname(__file__))
sys.path.append(parent_folder_path)
sys.path.append(os.path.join(parent_folder_path, 'lib'))
sys.path.append(os.path.join(parent_folder_path, 'plugin'))

from flowlauncher import FlowLauncher
import webbrowser
import pyperclip
from lorem_text import lorem

class HelloWorld(FlowLauncher):

    def query(self, query):
        new = query.lower()
        newNew = ""
        
        for char in new:
            if char.isdigit() or char.isupper():
                newNew += char        

        if (query != ""):
            if new[0] == 'p':
                if (newNew != ''):
                    num = int(newNew)
                    return [{
                        "Title": "Paragraphs: {fnum}".format(fnum = num),
                        "SubTitle": "Generate {fnum} paragraphs".format(fnum = num),
                        "IcoPath": "Images/app.png",
                        "score": 1,
                        "JsonRPCAction": {
                            "method": "genParagraph",
                            "parameters": [num],
                        }
                    }]
                else:
                    return [{
                        "Title": "Paragraph",
                        "SubTitle": "Add a number to specify the amount of paragraphs you want to generate.",
                        "IcoPath": "Images/app.png",
                        "score": 1,
                        "JsonRPCAction": {
                            "method": "genParagraph",
                            "parameters": [1],
                        }
                    }]
            elif new[0] == 's':
                return [
                    {
                        "Title": "Sentence",
                        "SubTitle": "Can only generate a sentence.",
                        "IcoPath": "Images/app.png",
                        "score": 2,
                        "JsonRPCAction": {
                            "method": "genSentence",
                            "parameters": [1],
                        }
                    }]
                
            elif new[0] == 'w':
                if (newNew != ''):
                    num = int(newNew)
                    return [
                        {
                            "Title": "Words: {fnum}".format(fnum = num),
                            "SubTitle": "Generate {fnum} words".format(fnum = num),
                            "IcoPath": "Images/app.png",
                            "score": 0,
                            "JsonRPCAction": {
                                "method": "genWord",
                                "parameters": [1],
                            }
                        }]
                else:
                    return [
                        {
                            "Title": "Word",
                            "SubTitle": "Add a number to specify the amount of words you want to generate .",
                            "IcoPath": "Images/app.png",
                            "score": 0,
                            "JsonRPCAction": {
                                "method": "genWord",
                                "parameters": [1],
                            }
                        }]
            else:
                return [
                    {
                        "Title": "Incorrect query",
                        "SubTitle": "Please follow the suggestions.",
                        "IcoPath": "Images/x.png",
                        "score": 2,
                    }]
        return [
            {
                "Title": "Sentence",
                "SubTitle": "Generate a sentence.                                 Shortcut: \"s\"",
                "IcoPath": "Images/app.png",
                "score": 2,
                "JsonRPCAction": {
                    "method": "genSentence",
                    "parameters": [1],
                }
            },
            {
                "Title": "Paragraph",
                "SubTitle": "Generate a paragraph / paragraphs.         Shortcut: \"p\"",
                "IcoPath": "Images/app.png",
                "score": 1,
                "JsonRPCAction": {
                    "method": "genParagraph",
                    "parameters": [1],
                }
            },
            {
                "Title": "Word",
                "SubTitle": "Generate a word / string of words.             Shortcut: \"w\"",
                "IcoPath": "Images/app.png",
                "score": 0,
                "JsonRPCAction": {
                    "method": "genWord",
                    "parameters": [1],
                }
            }
            ]
        


    def context_menu(self, data):
        return [
            {
                "Title": "Please give the plugin a star on GitHub",
                "SubTitle": "Every star is appreciated. Please 🥺",
                "IcoPath": "Images/star.png",
                "JsonRPCAction": {
                    "method": "open_url",
                    "parameters": ["https://github.com/Tomkov1c/Lorem-Ipsum-Generator"]
                }
            }
        ]

    def genSentence(self, num):
        if num != 0 or num != None: 
            temp = lorem.sentence()
        else: 
            temp = lorem.sentence()
        pyperclip.copy(temp)
    
    def genParagraph(self, num):
        if num != 0 or num != None: 
            temp = lorem.paragraphs(num)
        else: 
            temp = lorem.paragraphs(1)
        pyperclip.copy(temp)
    
    def genWord(self, num):
        if num != 0 or num != None: 
            temp = lorem.words(num)
        else: 
            temp = lorem.words(1)
        pyperclip.copy(temp)

    def open_url(self, url):
        webbrowser.open(url)
  

if __name__ == "__main__":
    HelloWorld()

