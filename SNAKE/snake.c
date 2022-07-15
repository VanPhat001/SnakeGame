#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#include <windows.h>

#define GREEN 10
#define RED 12
#define YELLOW 14
#define WHITE 15
#define MaxRow 17
#define MaxCol 40
#define speed 100

typedef struct {
	int x, y;
} Toado;

Toado snake[MaxCol * MaxRow];
Toado cursor, food;
int snake_size = 0;
int move = 3;


void textcolor(int ForgC) // thiet lap mau
{
    WORD wColor;
    HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    if(GetConsoleScreenBufferInfo(hStdOut, &csbi))
    {
        wColor = (csbi.wAttributes & 0xF0) + (ForgC & 0x0F);
        SetConsoleTextAttribute(hStdOut, wColor);
    }
    return;
}

void gotoxy(int x, int y)
{
    static HANDLE h = NULL;
    if(!h)
        h = GetStdHandle(STD_OUTPUT_HANDLE);
    COORD c = { x, y };
    SetConsoleCursorPosition(h,c);
}

void init_body(int size)
{
	gotoxy(0, 0);
	int i;
	textcolor(GREEN);
	for (i = 0; i < size; i++)
	{
		snake[i].y = 0;
		snake[i].x = i;
		printf("O");
	}
	cursor.x = size - 1;
	cursor.y = 0;
	snake_size = size;
}


void update_snake(int y, int x)
{
	int i;
	for (i = 0; i < snake_size - 1; i++)
	{
		snake[i] = snake[i + 1];
	}
	snake[snake_size - 1].x = x;
	snake[snake_size - 1].y = y;
}

int random(int a, int b)
{
	return rand() % (b - a + 1) + a;
}

int food_in_snake()
{
	int i;
	for (i = 0; i < snake_size; i++)
	{
		if (snake[i].x == food.x && snake[i].y == food.y) return 1;
	}
	return 0;
}

void create_food()
{
	do {
		food.y = random(0, MaxRow - 1);
		food.x = random(0, MaxCol - 1);
	} while (food_in_snake());
	gotoxy(food.x, food.y);
	textcolor(YELLOW);
	printf("X");
}

void init()
{
	init_body(5);
	create_food();
}

int end_game()
{
	int i;
	for (i = 0; i < snake_size; i++)
	{
		if (snake[i].x == cursor.x && snake[i].y == cursor.y)
		{
			return 1;
		}
	}
	return 0;
}

int moveX[] = {0, -1, 1, 0};
int moveY[] = {-1, 0, 0, 1};
void play_game()
{
	init();
	int timeStart = time(NULL);
	while (1)
	{
		if (kbhit())
		{
			char ch;
			while (kbhit()) ch = getch();
			// process
			switch(ch)
			{
				case '0':
					return;
				case 72:
					if (move != 4) move = 1;
					break;
				case 75:
					if (move != 3) move = 2;
					break;
				case 77:
					if (move != 2) move = 3;
					break;
				case 80:
					if (move != 1) move = 4;
					break;
			}
		}
		// update 
		cursor.x += moveX[move - 1];
		cursor.y += moveY[move - 1];
		
		// check and replace
		if (cursor.x < 0) cursor.x = MaxCol - 1;
		if (cursor.x > MaxCol - 1) cursor.x = 0;
		if (cursor.y < 0) cursor.y = MaxRow - 1;
		if (cursor.y > MaxRow - 1) cursor.y = 0;
		
		if (end_game())
		{
			gotoxy(cursor.x, cursor.y);
			textcolor(RED);
			printf("O");
			gotoxy(0, MaxRow);
			printf("end game!!!");
			return;
		}
		
		if (cursor.x == food.x && cursor.y == food.y)
		{
			snake_size++;
			snake[snake_size - 1].x = cursor.x;
			snake[snake_size - 1].y = cursor.y;
			
			create_food();
		}
		
		// clear tail
		gotoxy(snake[0].x, snake[0].y);
		printf(" ");
		
		// print head
		textcolor(GREEN);
		gotoxy(cursor.x, cursor.y);
		printf("O");
		
		// chang snake coord
		update_snake(cursor.y, cursor.x);
		
		int timePlay = time(NULL) - timeStart;
		textcolor(WHITE);
		gotoxy(0, MaxRow);
		printf("Time: %02d:%02d ___ Snake size : %d", timePlay/60, timePlay%60, snake_size);
		
		Sleep(speed);
	}
}

int main()
{
	srand(time(NULL));
	play_game();
	return 0;
}
