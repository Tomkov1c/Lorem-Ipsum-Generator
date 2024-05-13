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
        
        if (newNew != ""):
            finished = int(newNew)
            syntax = new[0]
            if syntax == 'p':
                return [
                {
                    "Title": "Paragraphs: {fnum}".format(fnum = finished),
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 1,
                    "JsonRPCAction": {
                        "method": "genParagraph",
                        "parameters": [finished]
                    }
                    
                },
                {
                    "Title": "Words: {fnum}".format(fnum = finished),
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 0, # !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    "JsonRPCAction": {
                        "method": "genWord",
                        "parameters": [finished]
                    }
                }
            ]
            else: 
                return [
                {
                    "Title": "Words: {fnum}".format(fnum = finished),
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 1,
                    "JsonRPCAction": {
                        "method": "genWord",
                        "parameters": [finished]
                    },
                },
                {
                    "Title": "Paragraphs: {fnum}".format(fnum = finished),
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 0,
                    "JsonRPCAction": {
                        "method": "genParagraph",
                        "parameters": [finished]
                    },
                }
            ]
        
        return [
                {
                    "Title": "Sentence",
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 2,
                    "JsonRPCAction": {
                        "method": "genSentence",
                        "parameters": [1]
                    }
                },
                {
                    "Title": "Paragraph",
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 1,
                    "JsonRPCAction": {
                        "method": "genParagraph",
                        "parameters": [1]
                    }
                },
                {
                    "Title": "Word",
                    "SubTitle": "This is where your subtitle goes, press enter to open Flow's url",
                    "IcoPath": "Images/app.png",
                    "score": 0,
                    "JsonRPCAction": {
                        "method": "genWord",
                        "parameters": [1]
                    }
                }
        ]

    def context_menu(self, data):
        return [
            {
                "Title": "Hello World Python's Context menu",
                "SubTitle": "Press enter to open Flow the plugin's repo in GitHub",
                "IcoPath": "Images/app.png",
                "JsonRPCAction": {
                    "method": "open_url",
                    "parameters": ["https://github.com/Flow-Launcher/Flow.Launcher.Plugin.HelloWorldPython"]
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

    
    
    
    
    def paragraphs(self):
        i = 0
        

if __name__ == "__main__":
    HelloWorld()

