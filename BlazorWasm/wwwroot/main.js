console.log("Hello World!")

function init() {
    document.getElementById("canvas").appendChild(canvas)
    gl = document.getElementById("gl").getContext("webgl2")
}

var scene = () =>
    Scene.create(800,600,1)

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

window.canvasFocus = () => {
    document.getElementById("canvas").focus()
}

function toggleFullScreen() {
    if (!document.fullscreenElement)
        document.documentElement.requestFullscreen()
    else if (document.exitFullscreen)
        document.exitFullscreen()
    screen.orientation.lock('portrait')
}