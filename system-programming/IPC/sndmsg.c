/* Kullanım:program_adı anahtar tip boşluksuz_bir_mesaj */ 

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/wait.h>

/* msgbuf veri yapısı types.h header dosyasında tanımlıdır*/
struct msgbuf {
  long mtype;
  char mtext[100];
};

int main(int argc, char * argv[]) {
  int msid, v;
  struct msgbuf mess;

  if (argc != 4) {
    printf("Kullanım: <key> <tip> <text>\n");
    exit(1);
  }
  /* mesaj kuyruğunu yaratma veya erişim sağlama */
  msid = msgget((key_t) atoi(argv[1]), IPC_CREAT | 0666);
  if (msid == -1) {
    printf("Mesaj kuyruğunu elde edemedi\n");
    exit(1);
  }
  printf("msid: %d\n", msid);

  /* Komut satırından mesajı hazırlayalım */
  mess.mtype = atoi(argv[2]); /* mesaj tipi */
  strcpy(mess.mtext, argv[3]); /* mesajın metni */

  /* mesajı kuyruğa gönder */
  v = msgsnd(msid, & mess, strlen(argv[3]) + 1, 0);
  if (v < 0) printf("HATA : Mesaj kuyruğuna yazılamadı\n");
  printf("Gönderici sonlandı\n");
  printf("v: %d\n", v);
  exit(0);
}