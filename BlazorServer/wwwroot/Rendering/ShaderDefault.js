const vertex = `#version 300 es
    #define PI 3.1415926535897932384626433832795
    #define ASPECT_RATIO 16.0/9.0
    uniform float angle;
    uniform int index;
    uniform vec2 camera;
    uniform vec2 position;
    uniform vec2 size;
    uniform vec3 dimensions;
    in vec2 vertex_position;


    mat3 rotation(float degrees) {
    float angle = radians(degrees);
    float c = cos(angle);
    float s = sin(angle);
    return mat3(
    vec3(c, -s, 0),
    vec3(s,  c, 0),
    vec3(0,  0, 1));
}

mat3 scaling(vec2 size) {
    return mat3(
        vec3(size.x,      0, 0),
        vec3(     0, size.y, 0),
        vec3(     0,			0, 1));
}

void main() {
    mat3 rot = rotation(angle);
    mat3 scale = scaling(vec2(dimensions.z, dimensions.z)) * scaling(size);

    vec3 resolution = vec3(dimensions.xy, -1.0);
    vec3 cam = vec3(camera, 0) / resolution;
    vec3 pos = vec3(position, float(index)/100.0) / resolution;
    vec3 vertex = vec3(vertex_position, 1.0);
    vec3 vertex_aspect = vertex * rot / vec3(ASPECT_RATIO, 1.0, 1.0);
    gl_Position = vec4(vertex_aspect * scale + pos + cam - 1.0, 1.0);
}`

const fragment = `#version 300 es
precision mediump float;
uniform float alpha;
uniform vec3 color;
out vec4 outColor;

void main() {
    outColor = vec4(color, alpha);
}`

const ShaderDefault = {
    vertex,
    fragment
}