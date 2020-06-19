import sys
import time
import logging

#from watchdog.observers import Observer
#from watchdog.events import ImagesEventHandler

import tkinter as tk
import tkinter.filedialog as tkfiledialog

class Application(tk.Frame):

    def __init__(self, master=None):
        super().__init__(master)
        self.master = master
        self.pack()
        self.create_widgets()
        self.after(100, self.tick)


    def create_widgets(self):
        self.hi_there = tk.Button(self)
        self.hi_there["text"] = "Hello World\n(click me)"
        self.hi_there["command"] = self.say_hi
        self.hi_there.pack(side="top")

        self.quit = tk.Button(self, text="QUIT", fg="red",
                              command=self.master.destroy)
        self.quit.pack(side="bottom")

    def say_hi(self):
        print("hi there, everyone!")

    def tick(self):
        print("tick tock")
        self.after(100, self.tick)

# Run Code

root = tk.Tk()
app = Application(master=root)
app.mainloop()