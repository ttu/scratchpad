from flask import Flask, jsonify, request, abort

app = Flask(__name__)

# Sample data
tasks = [
    {
        'id': 1,
        'title': u'Buy groceries',
        'description': u'Milk, Cheese, Pizza, Fruit, Tylenol',
        'done': False
    },
    {
        'id': 2,
        'title': u'Learn Python',
        'description': u'Need to find a good Python tutorial on the web',
        'done': False
    }
]


@app.route('/')
def index():
    return "Hello, World!"


@app.route('/tasks/', methods=['GET'])
def get_tasks():
    return jsonify({'tasks': tasks})


@app.route('/tasks/<int:task_id>', methods=['GET'])
def get_task(task_id):
    # task = [i for i in tasks if i.["id"] == task_id]
    task = filter(lambda x: x["id"] == task_id, tasks)
    return jsonify({'task': task})


@app.route('/tasks/', methods=['POST'])
def create_task():
    task = {
        'id': tasks[-1]['id'] + 1,
        'title': request.json['title'],
        'description': request.json['description'],
        'done': False
    }
    tasks.append(task)
    return jsonify({'task': task}), 201


@app.route('/tasks/<int:task_id>', methods=['PUT'])
def update_task(task_id):
    task = filter(lambda x: x["id"] == task_id, tasks)
    if len(task) == 0:
        abort(404)
    task = task[0]
    task['title'] = request.json['title']
    task['description'] = request.json['description']
    return jsonify({'task': task}), 201


@app.route('/tasks/<int:task_id>', methods=['DELETE'])
def delete_task(task_id):
    task = [task for task in tasks if task['id'] == task_id]
    if len(task) == 0:
        abort(404)
    tasks.remove(task[0])
    return jsonify({'result': True})


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)
