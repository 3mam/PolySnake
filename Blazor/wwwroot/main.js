console.log("Hello World!")

function init() {
    gl = document.getElementsByTagName("canvas")[0].getContext("webgl2")
}

var scene = () =>
    Scene.create(800,600,1)

var key = ""
document.addEventListener('keypress', (event) => {
    key = event.key
})

document.addEventListener('keyup', (event) => {
    key = ""
})

function loop() {
    let last = 0
    const insideLoop = timestamp => {
        const delta = Math.min(1, (timestamp - last) / 1000)
        last = timestamp
        DotNet.invokeMethodAsync('Blazor', 'Run', delta, key)
        window.requestAnimationFrame(insideLoop)
    }
    insideLoop(0)
}


function toggleFullScreen() {
    if (!document.fullscreenElement)
        document.documentElement.requestFullscreen()
    else if (document.exitFullscreen)
        document.exitFullscreen()
    screen.orientation.lock('portrait')
}