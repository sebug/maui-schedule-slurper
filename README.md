# MAUI Schedule Slurper
This project uses [URLSession](https://developer.apple.com/documentation/foundation/urlsession) to make multiple requests to load sessions.

I'm looking to slurp https://fosdem.org/2024/schedule/ but in a civilized way,
meaning I don't want to put undue stress on the web site. URLSession is the
perfect solution here, I'll schedule it and store existing entries in the
database, continuing as we go.

