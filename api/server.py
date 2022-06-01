# Server Flask
from flask import Flask
import os

def command_pull():
    os.system('cd ../data/magica && git pull')

server = Flask(__name__)

@server.route('/api/v1/pull', methods=['GET'])
def pull():
    # execute command and return json
    command_pull()
    return '{"status": 200, "message": "OK"}'


server.run(host='0.0.0.0', port=5000)
