shader_type canvas_item;

uniform float amplitude=130;
instance uniform float time : hint_range(0, 1, 0.01) = 0;
uniform float attackTime : hint_range(0, 1, 0.01) = 0.35;
uniform vec4 bezierParams = vec4(0.53,-2,0.23,1.67);

vec2 bezier_quad(vec2 a, vec2 b, float t){
	vec2 p1 = mix(vec2(0,0),a,t);
	vec2 p2 = mix(a,b,t);
	vec2 p3 = mix(b,vec2(1,1),t);
	
	return mix( mix(p1,p2,t), mix(p2,p3,t), t);
}

void vertex() {
	// Called for every vertex the material is visible on.
	vec2 pos = VERTEX;
	
	float t = float(0);
	//VERTEX = vec2(pos.x,pos.y)
	if(time<attackTime){
		t = bezier_quad(vec2(bezierParams.x,bezierParams.y),vec2(bezierParams.z,bezierParams.a),time/attackTime).y;
	}
	else{
		t=mix(bezier_quad(vec2(bezierParams.x,bezierParams.y),vec2(bezierParams.z,bezierParams.a),1).y,0, (time-attackTime) / (float(1)-attackTime));
	}
	float s = t * amplitude;
	
	pos += vec2(s*float(-1),s) * (UV - vec2(0.5,0.5));
	VERTEX = pos;
}

void fragment() {
	// Called for every pixel the material is visible o
	COLOR = texture(TEXTURE, UV);
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
