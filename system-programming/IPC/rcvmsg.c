/* Kullanım:program_adı anahtar tip */
/* Usage: rcvmsg key type */

#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <errno.h>
#include <sys/wait.h>

struct msgbuf {
  long mtype;
  char mtext[100];
};

int main(int argc, char * argv[]) {
  int msid, v;
  struct msgbuf mess;
  if (argc != 3) {
    printf("Kullanım: <key> <tip>\n");
    exit(1);
  }

  /* mesaj kuyruğu handle ını al */
  msid = msgget((key_t) atoi(argv[1]), 0);
  if (msid == -1) {
    printf("Bu key ile mesaj kuyruğuna erişim sağlanamadı\n");
    exit(1);
  }

  /* kuyruktan bir mesaj al, mesaj kuyruktan silinmiş olur */
  v = msgrcv(msid, (struct msgbuf * ) & mess, 100, atoi(argv[2]), IPC_NOWAIT);
  if (v < 0) {
    if (errno == ENOMSG)
      printf("Belirtilen tipte mesaj kuyrukta yok\n");

    else printf("HATA:kuyruktan okunamadı\n");
  } else printf("[%ld] %s\n", mess.mtype, mess.mtext);

  if (msgctl(msid, IPC_RMID, 0) < 0)
    printf("Hata: mesaj kuyruğu sistemden silinemedi\n");
  exit(1);

  else printf("Mesaj kuyruğu (key = %d ) sistemden silindi\n", atoi(argv[1]));

  exit(0);
}