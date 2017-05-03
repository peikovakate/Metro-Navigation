-- Test task for MacPaw Internship 2017 --
Create an application “Metro Navigation” that allows users to determine to select two stations and get a visual animated route between them. 
You can choose any city metro all around the world to create your application. 
Upload your application project or project archive somewhere online (GitHub, Dropbox, Google Drive, etc.), and provide the link to it which will give us at least “read” access. 
Application requirements are: 
- it should be possible to build it on Windows using .NET framework with WPF; 
- if you used any opensource code parts, code sniplets, please, state in comments that it's not your original code and where did you get it from *

-- Description of an app --
Pattern - mvvm
Can be easily converted to any metro map by changing (adding) data files

-- Description of data files --
stations.csv:
Station id;Station name;id of line;x-position;y-position

connections.csv:
Station id;Station id;type of connection

lines.csv:
id of line;r;g;b
last 3 columns = color (RGB)

type of connection:
t - train
p - pedestrian

Kiev
2017