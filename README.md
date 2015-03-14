# Gouda 

**Gouda** is a .NET wrapper for the Ghostscript C API. 

Ghostscript is a popular Postscript interpreter and image renderer used to display, convert, and print 
Postscript and PDF files. This will allow access to Ghostscript without the need to shell out to `gswin32c.exe`. 
It will also allow you to create and use custom output devices for Ghostscript. 

The Ghostscript version that the wrapper is based on is GPL 8.60. Please check the 
[Ghostscript Home Page](http://www.ghostscript.com/) to obtain the latest version. 

Please note:
Although the wrapper code is offered under the MIT license (completely unrestricted) the Ghostscript is under GPL. 
This prevents use within commerical applications without a commerical license. 
However, if you have a valid commercial license for Ghostscript, you may use the wrapper code to access it from 
your commerical application with no licensing concerns.

## Current Status

Gouda is not yet to a first release. 

If you are interested in working on the code, feel free to get the latest version, make your changes, 
and send a patch in. If you want to do a lot of work on the project, drop a line to thoward37@gmail.com, 
and I'll set you up with commit access to the repository. A quick note indicating what you want to work 
on would be helpful, so we don't step each other's toes. 

## Outstanding Issues

  * Can't get the display callback struct to work out. Fails with error code -15 on init?
  * Currently writing the API extern calls and defining the structs. 
