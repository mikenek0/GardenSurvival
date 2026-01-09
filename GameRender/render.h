#ifndef RENDER_H
#define RENDER_H

#ifdef GAMERENDER_EXPORTS
#define API extern "C" __declspec(dllexport)
#else
#define API extern "C" __declspec(dllimport)
#endif

struct EnemyObject {
    float x;
    float y;
    int Id;
};

struct ExpOrbObject {
    float x;
    float y;
    int value; 
};

struct WeaponObject {
    float x;
    float y;
    float radius;
    int type;           
    float angle;        
    float life;        
};
struct TextObject {
    float x;
    float y;
    char text[32]; 
    int fontSize;
    float r, g, b; 
};

API bool InitOpenGL(void* hwnd);
API void ResizeOpenGL(int width, int height);
API void RenderFrame(float playerX, float playerY, bool isDamaged, const EnemyObject* enemies, int enemyCount, const WeaponObject* weapons, int weaponCount, const ExpOrbObject* orbs, int orbCount, const TextObject* texts, int textCount,
    int screenWidth, int screenHeight);
API void ShutdownOpenGL();
API void InitTileMap(int width, int height);

#endif