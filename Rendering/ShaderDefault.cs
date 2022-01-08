namespace PolySnake.Rendering;
public static class ShaderDefault
 {
   public const string Vertex = @"#version 400
	#define PI 3.1415926535897932384626433832795
	#define ASPECT_RATIO 1.77777777778
	uniform float angle;
	uniform float index;
	uniform vec2 camera;
	uniform vec2 position;
	in vec4 vertex_position;
	out vec2 color_index;

mat3 rotate(float degrees) {
	float angle = radians(degrees);
	float c = cos(angle);
	float s = sin(angle);
	return mat3(
	vec3(c, -s, 0),
	vec3(s,  c, 0),
	vec3(0,  0, 1));
}

void main() {
	mat3 rot = rotate(angle);

	mat3 scale = mat3(
	vec3(0.1,   0, 0),
	vec3(  0, 0.1, 0),
	vec3(  0,   0, 1));

	vec2 dimensions = vec2(1000, 500);
	vec2 cam = camera / dimensions;
	vec2 pos = position / dimensions;
	vec3 vertex = vec3(vertex_position.x, vertex_position.y, index);
	vec3 xyz = vertex * rot / vec3(ASPECT_RATIO, 1, 1);
	gl_Position = vec4((xyz * scale) + vec3((pos + cam) - 1, 0), 1);
	color_index = vec2(vertex_position.z, vertex_position.w);
}
"; 

   public const string Fragment = @"#version 400
precision mediump float;
uniform sampler2D palette;
uniform int switch_palette;
uniform float alpha;
out vec4 outColor;
in vec2 color_index;

void main() {
	int c = int(color_index.x);
	float a = color_index.y;
	outColor = texelFetch(palette, ivec2(c, switch_palette), 0) * vec4(1, 1, 1, a) * vec4(1, 1, 1, alpha);
}
";
}