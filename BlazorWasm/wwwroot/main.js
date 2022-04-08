console.log('Hello World!')

var gl = null
var canvas = document.createElement('canvas')
canvas.id = 'gl'
canvas.width = 640
canvas.height = 360

function init() {
    document.getElementById('canvas').appendChild(canvas)
    gl = document.getElementById('gl').getContext('webgl2')
}

var scene = (width, height, scale) =>
    new Scene(width, height, scale)

function loop() {
    let last = 0
    const insideLoop = timestamp => {
        const delta = Math.min(1, (timestamp - last) / 1000)
        last = timestamp
        DotNet.invokeMethodAsync('BlazorWasm', 'Run', delta)
        window.requestAnimationFrame(insideLoop)
    }
    insideLoop(0)
}

function canvasFocus() {
    document.getElementById('canvas').focus()
}