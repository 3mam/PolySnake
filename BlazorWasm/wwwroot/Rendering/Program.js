function loadShader(code, type) {
    const shader = gl.createShader(type)
    gl.shaderSource(shader, code)
    gl.compileShader(shader)
    if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
        console.log(gl.getShaderInfoLog(shader))
        return -1
    }
    return shader
}

const Program = {
    load: (vertex, fragment) => {
        const verShader = loadShader(vertex, gl.VERTEX_SHADER)
        const fragShader = loadShader(fragment, gl.FRAGMENT_SHADER)
        const id = gl.createProgram()
        gl.attachShader(id, verShader)
        gl.attachShader(id, fragShader)
        gl.linkProgram(id)

        return id
    }
}