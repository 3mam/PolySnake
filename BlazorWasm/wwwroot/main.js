console.log("Hello World!")

const width = 640
const height = 360
var gl = null
var canvas = document.createElement('canvas')
canvas.id = "gl"
canvas.width = width
canvas.height = height
canvas.style.position = "absolute"

function init() {
    document.getElementById("canvas").appendChild(canvas)
    gl = document.getElementById("gl").getContext("webgl2")
}

var scene = () =>
    new Scene(width, height, 1)

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
    document.getElementById("canvas").focus()
}