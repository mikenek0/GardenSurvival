// GameRender.cpp
#include "pch.h"
#include "render.h"
#include <windows.h>
#include <string>
#include <math.h>
#include <gl/GL.h>
#include <gl/GLU.h>

#define STB_IMAGE_IMPLEMENTATION
#include "stb_image.h"

#pragma comment(lib, "opengl32.lib")
#pragma comment(lib, "glu32.lib")
using namespace std;
static HDC  g_hDC = nullptr;
static HGLRC g_hRC = nullptr;
static GLuint g_playerTexture = 0;
static GLuint g_enemyTexture = 0;
static GLuint g_orbTexture = 0;

const double M_PI = 3.14159;
#define TILE_SIZE 64.0f
#define TILE_PER_ROW 9.0f

static GLuint g_tileTexture = 0;
static int g_mapWidth = 0;
static int g_mapHeight = 0;
static int* g_tileMap = nullptr;

static float g_cameraX = 0.0f;
static float g_cameraY = 0.0f;


static bool SetupPixelFormat(HDC hDC) {
    PIXELFORMATDESCRIPTOR pfd = {
        sizeof(PIXELFORMATDESCRIPTOR),
        1,
        PFD_DRAW_TO_WINDOW | PFD_SUPPORT_OPENGL | PFD_DOUBLEBUFFER,
        PFD_TYPE_RGBA,
        24,
        0, 0, 0, 0, 0, 0,
        0,
        0,
        0,
        0, 0, 0, 0,
        32,
        0,
        0,
        PFD_MAIN_PLANE
    };

    int pixelFormat = ChoosePixelFormat(hDC, &pfd);
    if (!pixelFormat) return false;
    return SetPixelFormat(hDC, pixelFormat, &pfd) != FALSE;
}

static GLuint LoadTexture(const char* filename) {
    int width, height, channels;


    stbi_set_flip_vertically_on_load(true);

    unsigned char* data = stbi_load(filename, &width, &height, &channels, 0);
    if (!data) {
        OutputDebugStringA("Ошибка загрузки текстуры\n");
        return 0;
    }

    GLuint textureID;
    glGenTextures(1, &textureID);
    glBindTexture(GL_TEXTURE_2D, textureID);

    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP);
    glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP);

    GLenum format = (channels == 4) ? GL_RGBA : GL_RGB;
    glTexImage2D(GL_TEXTURE_2D, 0, format, width, height, 0, format, GL_UNSIGNED_BYTE, data);

    stbi_image_free(data);
    return textureID;
}

static void DrawExpOrb(float x, float y, float size, GLuint textureID, float r = 1.0f, float g = 1.0f, float b = 1.0f) {
    if (textureID) {
        glEnable(GL_TEXTURE_2D);
        glBindTexture(GL_TEXTURE_2D, textureID);
        glColor3f(r, g, b);

        glBegin(GL_QUADS);
        glTexCoord2f(0.0f, 0.0f); glVertex2f(x - size, y - size);
        glTexCoord2f(1.0f, 0.0f); glVertex2f(x + size, y - size);
        glTexCoord2f(1.0f, 1.0f); glVertex2f(x + size, y + size);
        glTexCoord2f(0.0f, 1.0f); glVertex2f(x - size, y + size);
        glEnd();

        glDisable(GL_TEXTURE_2D);
    }
    
}
static GLuint g_fontList = 0;

static void SetupFont() {
    HDC hDC = wglGetCurrentDC();
    HFONT hFont = CreateFont(18, 0, 0, 0, FW_BOLD, FALSE, FALSE, FALSE, ANSI_CHARSET,
        OUT_TT_PRECIS, CLIP_DEFAULT_PRECIS, CLEARTYPE_QUALITY,
        DEFAULT_PITCH | FF_DONTCARE, "Arial");

    SelectObject(hDC, hFont);
    wglUseFontBitmaps(hDC, 0, 255, 1000);
    g_fontList = 1000;
}

static void DrawText(const char* text, float x, float y, float r, float g, float b) {
    if (!g_fontList) {
        SetupFont();
    }

    glColor3f(r, g, b);
    glRasterPos2f(x, y);

    for (const char* c = text; *c != '\0'; c++) {
        glCallList(g_fontList + *c);
    }
}

bool InitOpenGL(void* hwnd) {
    HWND hWnd = (HWND)hwnd;
    g_hDC = GetDC(hWnd);
    if (!g_hDC) return false;

    if (!SetupPixelFormat(g_hDC)) {
        ReleaseDC(hWnd, g_hDC);
        return false;
    }

    g_hRC = wglCreateContext(g_hDC);
    if (!g_hRC) {
        ReleaseDC(hWnd, g_hDC);
        return false;
    }

    if (!wglMakeCurrent(g_hDC, g_hRC)) {
        wglDeleteContext(g_hRC);
        ReleaseDC(hWnd, g_hDC);
        return false;
    }

    glClearColor(0.05f, 0.05f, 0.1f, 1.0f);
    glDisable(GL_DEPTH_TEST);
    glEnable(GL_BLEND);
    glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
    
    g_tileTexture = LoadTexture("pic\\Tale.png");
    g_playerTexture = LoadTexture("pic\\player.png");
    g_enemyTexture = LoadTexture("pic\\enemy.png");
    g_orbTexture = LoadTexture("pic\\orb.png");
    SetupFont();
    return true;
}

void ResizeOpenGL(int width, int height) {
    if (width <= 0) width = 1;
    if (height <= 0) height = 1;

    glViewport(0, 0, width, height);

    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();

    gluOrtho2D(0, width, height, 0);
    glMatrixMode(GL_MODELVIEW);
}

static void DrawWeapon(const WeaponObject* weapon) {
    if (weapon->type == 1) { 
        glLineWidth(3.5f);
        float maxRadius = weapon->radius;
        for (int ring = 0; ring < 3; ring++) {
            float radius = maxRadius * (0.4f + 0.3f * ring);
            float alpha = 0.3f + 0.2f * ring;
            glColor4f(0.5f, 0.0f, 0.5f, alpha);
            glBegin(GL_LINE_LOOP);
            for (int i = 0; i <= 32; i++) {
                float angle = 2.0f * 3.14159f * i / 32;
                glVertex2f(weapon->x + radius * cos(angle),
                    weapon->y + radius * sin(angle));
            }
            glEnd();
        }
        glLineWidth(1.0f);
    }
    else if (weapon->type == 2) { 
        if (weapon->life <= 0) return;
        glLineWidth(5.0f);
        float alpha = weapon->life / 0.25f;
        if (alpha > 1.0f) alpha = 1.0f;
        glColor4f(1.0f, 0.3f, 0.3f, alpha);

        float dirRad = weapon->angle * M_PI / 180.0f;

        const float ARC_DEGREES = 30.0f;
        const float ARC_RAD = ARC_DEGREES * M_PI / 180.0f;
        const float HALF_ARC = ARC_RAD * 0.5f;

        float startAngle = dirRad - HALF_ARC; 
        float endAngle = dirRad + HALF_ARC; 

        float t = 1.0f - (weapon->life / 0.25f); 
        if (t < 0.0f) t = 0.0f;
        if (t > 1.0f) t = 1.0f;

        float currentAngle = startAngle + t * (endAngle - startAngle);

        float endX = weapon->x + weapon->radius * cos(currentAngle);
        float endY = weapon->y + weapon->radius * sin(currentAngle);

        glBegin(GL_LINES);
        glVertex2f(weapon->x, weapon->y);     
        glVertex2f(endX, endY);                
        glEnd();
        glLineWidth(1.0f);
    }
}

struct RenderEnemy {
    float x, y;
    int type;          
    bool facingRight;  
};
#define ENEMY_TYPES 4     
#define ENEMY_COLUMNS 2  

static void DrawEnemy(const RenderEnemy* e)
{
    if (!g_enemyTexture) return;

    glEnable(GL_TEXTURE_2D);
    glBindTexture(GL_TEXTURE_2D, g_enemyTexture);
    glColor3f(1.0f, 1.0f, 1.0f);

    float frameWidth = 1.0f / ENEMY_COLUMNS; 
    float frameHeight = 1.0f / ENEMY_TYPES;  

    int column = e->facingRight ? 1 : 0;
    int row = e->type % ENEMY_TYPES;

    float u0 = frameWidth * column;
    float u1 = u0 + frameWidth;
    float v1 = 1.0f - (frameHeight * row);
    float v0 = v1 - frameHeight;

    float size = 20.0f;

    glBegin(GL_QUADS);
    glTexCoord2f(u0, v1); glVertex2f(e->x - size, e->y - size);
    glTexCoord2f(u1, v1); glVertex2f(e->x + size, e->y - size);
    glTexCoord2f(u1, v0); glVertex2f(e->x + size, e->y + size);
    glTexCoord2f(u0, v0); glVertex2f(e->x - size, e->y + size);
    glEnd();

    glDisable(GL_TEXTURE_2D);
}

void InitTileMap(int width, int height)
{
    g_mapWidth = width;
    g_mapHeight = height;
    g_tileMap = (int*)malloc(sizeof(int) * width * height);

    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            g_tileMap[y * width + x] = 0; //трава
        }
    }

    // декор
    srand(12345);
    for (int i = 0; i < width * height / 6; i++)
    {
        int rx = rand() % width;
        int ry = rand() % height;
        int id = 1 + rand() % 8; 
        g_tileMap[ry * width + rx] = id;
    }

   
}

static void DrawTileMap(float offsetX, float offsetY, int screenWidth, int screenHeight)
{
    if (!g_tileTexture || !g_tileMap)
        return;

    glEnable(GL_TEXTURE_2D);
    glBindTexture(GL_TEXTURE_2D, g_tileTexture);
    glColor3f(1.0f, 1.0f, 1.0f);

    int startX = (int)(offsetX / TILE_SIZE);
    int startY = (int)(offsetY / TILE_SIZE);
    int endX = (int)((offsetX + screenWidth) / TILE_SIZE) + 2;
    int endY = (int)((offsetY + screenHeight) / TILE_SIZE) + 2;

    if (startX < 0) startX = 0;
    if (startY < 0) startY = 0;
    if (endX > g_mapWidth) endX = g_mapWidth;
    if (endY > g_mapHeight) endY = g_mapHeight;

    float tileUSize = 1.0f / TILE_PER_ROW;
    float texelOffset = 0.001f;

    for (int y = startY; y < endY; y++)
    {
        for (int x = startX; x < endX; x++)
        {
            float px = floorf(x * TILE_SIZE - offsetX);
            float py = floorf(y * TILE_SIZE - offsetY);
            int tileId = 0;

            float u0 = tileUSize * tileId + texelOffset;
            float u1 = u0 + tileUSize - texelOffset;
            float v0 = 0.0f + texelOffset;
            float v1 = 1.0f - texelOffset;

            glBegin(GL_QUADS);
            glTexCoord2f(u0, v1); glVertex2f(px, py);
            glTexCoord2f(u1, v1); glVertex2f(px + TILE_SIZE, py);
            glTexCoord2f(u1, v0); glVertex2f(px + TILE_SIZE, py + TILE_SIZE);
            glTexCoord2f(u0, v0); glVertex2f(px, py + TILE_SIZE);
            glEnd();
        }
    }

    for (int y = startY; y < endY; y++)
    {
        for (int x = startX; x < endX; x++)
        {
            int tileId = g_tileMap[y * g_mapWidth + x];
            if (tileId <= 0) continue;

            float px = floorf(x * TILE_SIZE - offsetX);
            float py = floorf(y * TILE_SIZE - offsetY);

            float u0 = tileUSize * tileId + texelOffset;
            float u1 = u0 + tileUSize - texelOffset;
            float v0 = 0.0f + texelOffset;
            float v1 = 1.0f - texelOffset;

            glBegin(GL_QUADS);
            glTexCoord2f(u0, v1); glVertex2f(px, py);
            glTexCoord2f(u1, v1); glVertex2f(px + TILE_SIZE * 0.6, py);
            glTexCoord2f(u1, v0); glVertex2f(px + TILE_SIZE * 0.6, py + TILE_SIZE * 0.6);
            glTexCoord2f(u0, v0); glVertex2f(px, py + TILE_SIZE * 0.6);
            glEnd();
        }
    }

    glDisable(GL_TEXTURE_2D);
}
static void DrawPlayer(float x, float y, float size, bool facingRight, bool isDamaged)
{
    if (!g_playerTexture) return;

    glEnable(GL_TEXTURE_2D);
    glBindTexture(GL_TEXTURE_2D, g_playerTexture);
    glColor3f(1.0f, 1.0f, 1.0f);

    const int columns = 3;
    const int rows = 2;
    const float frameWidth = 1.0f / columns;
    const float frameHeight = 1.0f / rows;

    int row = isDamaged ? 1 : 0;

    int column = facingRight ? 2 : 0;

    float u0 = frameWidth * column;
    float u1 = u0 + frameWidth;
    float v1 = 1.0f - (frameHeight * row);
    float v0 = v1 - frameHeight;

    glBegin(GL_QUADS);
    glTexCoord2f(u0, v1); glVertex2f(x - size, y - size);
    glTexCoord2f(u1, v1); glVertex2f(x + size, y - size);
    glTexCoord2f(u1, v0); glVertex2f(x + size, y + size);
    glTexCoord2f(u0, v0); glVertex2f(x - size, y + size);
    glEnd();

    glDisable(GL_TEXTURE_2D);
}

void RenderFrame(
    float playerX, float playerY, bool isDamaged,
    const EnemyObject* enemies, int enemyCount,
    const WeaponObject* weapons, int weaponCount,
    const ExpOrbObject* orbs, int orbCount,
    const TextObject* texts, int textCount,
    int screenWidth, int screenHeight)
{
    if (!g_hDC || !g_hRC) return;

    glClear(GL_COLOR_BUFFER_BIT);

    float mapWidthPx = g_mapWidth * TILE_SIZE;
    float mapHeightPx = g_mapHeight * TILE_SIZE;

    g_cameraX = playerX - screenWidth / 2.0f;
    g_cameraY = playerY - screenHeight / 2.0f;

    if (g_cameraX < 0) g_cameraX = 0;
    if (g_cameraY < 0) g_cameraY = 0;
    if (g_cameraX > mapWidthPx - screenWidth) g_cameraX = mapWidthPx - screenWidth;
    if (g_cameraY > mapHeightPx - screenHeight) g_cameraY = mapHeightPx - screenHeight;

    // карта
    DrawTileMap(g_cameraX, g_cameraY, screenWidth, screenHeight);

    //сдвиг камеры
    glPushMatrix();
    glTranslatef(-g_cameraX, -g_cameraY, 0.0f);

    // враги
    static float lastX[4096] = { 0.0f };
    for (int i = 0; i < enemyCount; ++i)
    {
        RenderEnemy e{};
        e.x = enemies[i].x;
        e.y = enemies[i].y;
        e.type = enemies[i].Id;
        bool facingRight = (enemies[i].x >= lastX[i]);
        lastX[i] = enemies[i].x;
        e.facingRight = facingRight;

        DrawEnemy(&e);
    }

    // опыт
    for (int i = 0; i < orbCount; ++i)
        DrawExpOrb(orbs[i].x, orbs[i].y, 15.0f, g_orbTexture);

    // оружие
    for (int i = 0; i < weaponCount; ++i)
        DrawWeapon(&weapons[i]);

    static float lastPlayerX = 0.0f;
    static bool facingRight = true;
    if (playerX > lastPlayerX + 0.1f) facingRight = true;
    else if (playerX < lastPlayerX - 0.1f) facingRight = false;
    lastPlayerX = playerX;
    // игрок
    DrawPlayer(playerX, playerY, 28.0f, facingRight, isDamaged);

    glPopMatrix();

    for (int i = 0; i < textCount; ++i)
        DrawText(texts[i].text, texts[i].x, texts[i].y, texts[i].r, texts[i].g, texts[i].b);

    SwapBuffers(g_hDC);
}

void ShutdownOpenGL() {
    if (g_playerTexture) {
        glDeleteTextures(1, &g_playerTexture);
        g_playerTexture = 0;
    }
    if (g_enemyTexture) {
        glDeleteTextures(1, &g_enemyTexture);
        g_enemyTexture = 0;
    }

    if (g_hRC) {
        wglMakeCurrent(nullptr, nullptr);
        wglDeleteContext(g_hRC);
    }
    if (g_hDC) {
        ReleaseDC(WindowFromDC(g_hDC), g_hDC);
        g_hDC = nullptr;
    }
    g_hRC = nullptr;
}