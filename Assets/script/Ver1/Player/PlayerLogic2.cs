using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerLogic2 : MonoBehaviour
{
    
    [SerializeField] stonemove1 CM1;
    [SerializeField] stonemove2 CM2;

    [SerializeField] GameData GData;

    //========================
    //infoの変更を自分でやっちゃう
    [SerializeField] GameObject phase1to2Prefab;
    [SerializeField] GameObject phase2Prefab;
    //=========================

    //見た目まわりの変更
    [SerializeField] InfomationLogic2 IL2;

    //CCボタン
    [SerializeField] Button BCCButton1;
    [SerializeField] Button BCCButton2;

    [SerializeField] Button WCCButton1;
    [SerializeField] Button WCCButton2;

    //色を消していく(安地)
    [SerializeField] colordelete CD;

    //黒石の番号
    string stoneNumB;

    //白石の番号
    string stoneNumW;

    //1チェックのマスの番号
    string NowmassNum;

    //2チェックのマスの番号
    string NextmassNum;

    //マスの座標
    Vector3 massPosition;

    // startのフラグ sequenceと統合できそう
    bool start=true;

    //ミルのフラグ
    bool millflag=false;

    //得点
    int Bscore=0;

    int Wscore=0;
    
    Color winner;


    //ターン管理
    // int turn=1;

    // enumを使ってターンに名前をつける
    enum COLOR
    {
        BLACK,  //黒色
        WHITE   //白色
    }

    //ターン
    COLOR turn = COLOR.BLACK;

    //ゲッタ
    int Nowturn(){
        int t=99;
        if(turn==COLOR.BLACK)   t=1;//黒
        else if(turn==COLOR.WHITE) t=2;//白

        return t;
    }


    //シークエンス管理
    int sequence=0;

    //2-1で間違っていた場合のフラグ
    bool errorflag1=false;


    //メソッド
    //=========================================================

    void Update(){
        if(start){//疑似スタート関数
            Changef2Info();
        }else{
            if(sequence==1) CheckTag1();
            else if(sequence==2) CheckTag2();
            else if(sequence==36) Mill(Nowturn());
        }
    }

    public void Changef2Info(){
        //UIの変更
        //phase1を消して、phase2を出す。
        phase1to2Prefab.SetActive(false);
        phase2Prefab.SetActive(true);

        //最初に選んだマイカラーの更新
        IL2.SettingMycolor();

        //2PのUIを消す
        IL2.ChangeUIturn(2);//言わばWのターンが終了した状態がBの開始と同じ

        
        //疑似スタート関数をOFF
        start=false;
        sequence=1;
        
    }

    void CheckTag1()//番号をゲット！
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit hit in Physics.RaycastAll(ray)){
                if(hit.collider.gameObject.CompareTag("Bstone")){//コマの時、コマ番号を入手
                    stoneNumB = hit.collider.gameObject.name.Remove(0,6);
                    // Debug.Log(stoneNumB);
                }else if(hit.collider.gameObject.CompareTag("Wstone")){
                    stoneNumW = hit.collider.gameObject.name.Remove(0,6);
                    // Debug.Log(stoneNumW);
                }else if(hit.collider.gameObject.CompareTag("mass")){//マスを入手
                    NowmassNum = hit.collider.gameObject.name.Remove(0,5);
                    Debug.Log(NowmassNum);
                }
                // else{
                //     errorflag1=true;
                //     // NowmassNum="null";//バグ潰し
                // }
            }

            //最後に確認
            sequence=2;

            // Debug.Log("<color=blue>エラーフラッグなう1</color>"+errorflag1);

            if(MatigattaKurikkuKorosu()){//想定外のマスはやり直し。敵かマスをクリック
                // Debug.Log("<color=blue>エラーフラッグなう2</color>"+errorflag1);
                CollateMyColor();//Myカラーをちゃんと選んでいるか
                // Debug.Log("<color=blue>エラーフラッグなう3</color>"+errorflag1);
                Checkmovablemass(int.Parse(NowmassNum));//移動可能マスを確認する
                // Debug.Log("<color=blue>エラーフラッグなう4</color>"+errorflag1);
                CheckNomovablemass();//その上で移動可能マスがmovableかの確認を行う
                // Debug.Log("<color=blue>エラーフラッグなう5</color>"+errorflag1);
            }


            if(errorflag1){//エラーフラグが立てば全消去
                stoneNumB=null;
                stoneNumW=null;
                NowmassNum=null;
                GData.movable.Clear();//movableの削除(消さないとmopvabaleが増える)
                sequence=1;//戻す
                errorflag1=false;
            }else{//正常
                //点滅
                if(turn==COLOR.BLACK){
                    // Debug.Log("色変えます");
                    CM1.stoneshine1(int.Parse(stoneNumB));//点滅
                }else if(turn==COLOR.WHITE){
                    CM2.stoneshine2(int.Parse(stoneNumW));//点滅
                }
            }
        }
        
    }

    bool MatigattaKurikkuKorosu(){//間違ったクリックしてなければ殺すし、次に進ませない
        //ターン無視は殺す
        if(turn==COLOR.BLACK){//Bのターン
            if(stoneNumB!=null&&NowmassNum!=null){//黒に値が入っててマスもクリックされている状態なら進んで良い
                Debug.Log("ターン無視してない@korosu");
                return true;
            }else{
                Debug.Log("駄目@korosu");
                errorflag1=true;
                return false;
            }
            // if(stoneNumB==null||stoneNumW!=null||NowmassNum!=null){//コマクリックしてない||白をクリックしてる
            //     errorflag1=true;
            //     Debug.Log("1に戻る:黒コマクリックしてない||白をクリックしてる");
            // }
        }else if(turn==COLOR.WHITE){//Wのターン
            if(stoneNumW!=null&&NowmassNum!=null){//黒に値が入っててマスもクリックされている状態なら進んで良い
                Debug.Log("ターン無視してない@korosu");
                return true;
            }else{
                Debug.Log("駄目@korosu");
                errorflag1=true;
                return false;
            }
            // if(stoneNumW==null||stoneNumB!=null||NowmassNum!=null){//コマクリックしてない||白をクリックしてる
            //     errorflag1=true;
            //     Debug.Log("1に戻る:コマクリックしてない||白をクリックしてる");
            // }
        }
        // Debug.Log("チェック完了korosu");

        return false;
    }

    //Mycolorの照合
    void CollateMyColor(){
        //もしflagが立ってたらそもそも判定を行わない
        if(errorflag1){
            Debug.Log("既にエラーなので見送り");
        }else{
            if(turn==COLOR.BLACK){//Bのターン
                if(GData.MycolorB!=GData.masscolorarray[int.Parse(NowmassNum)]&&Color.gray!=GData.masscolorarray[int.Parse(NowmassNum)]){//MYカラーと違ってかつ、グレーマスじゃない
                    CM1.Returnstonecolor1(int.Parse(stoneNumB));//色戻す
                    errorflag1=true;
                    Debug.Log("1に戻る：選んだコマがmycolorと合ってないかグレーマスじゃない");
                }
            }else if(turn==COLOR.WHITE){//Wのターン
                if(GData.MycolorW!=GData.masscolorarray[int.Parse(NowmassNum)]&&Color.gray!=GData.masscolorarray[int.Parse(NowmassNum)]){//MYカラーと違ってかつ、グレーますじゃない
                    CM2.Returnstonecolor2(int.Parse(stoneNumW));//色戻す
                    errorflag1=true;
                    Debug.Log("1に戻る：選んだコマがmycolorと合ってないかグレーマスじゃない");
                }
            }
        }


        Debug.Log("チェック完了mycolorの判定");
    }

    void Checkmovablemass(int x){//隣が空いてるかの確認
        //もしflagが立ってたらそもそも判定を行わない
        if(errorflag1){
            Debug.Log("既にエラーなので見送り");
        }else{
            //よこの確認
            if(x%6==0){//△の一番上。0の場合右は5になる
                if(GData.massdataArray[x+1]==0) GData.movable.Add(x+1);
                if(GData.massdataArray[x+5]==0) GData.movable.Add(x+5);
            }else if(x%6==5){//5...
                if(GData.massdataArray[x-5]==0) GData.movable.Add(x-5);
                if(GData.massdataArray[x-1]==0) GData.movable.Add(x-1);
            }else{//それ以外は横の確認
                if(GData.massdataArray[x+1]==0) GData.movable.Add(x+1);
                if(GData.massdataArray[x-1]==0) GData.movable.Add(x-1);
            }

            //たての確認
            if(x/6==0){
                if(GData.massdataArray[x+6]==0) GData.movable.Add(x+6);
            }else if(x/6==3){
                if(GData.massdataArray[x-6]==0) GData.movable.Add(x-6);
            }else{//1,2のとき
                if(GData.massdataArray[x+6]==0) GData.movable.Add(x+6);
                if(GData.massdataArray[x-6]==0) GData.movable.Add(x-6);
            }

            if(GData.movable.Count==0){//movableが0なら
                //点滅を戻す
                if(turn==COLOR.BLACK){//Bのターン
                    CM1.Returnstonecolor1(int.Parse(stoneNumB));//色戻す
                }else if(turn==COLOR.WHITE){//Wのターン
                    CM2.Returnstonecolor2(int.Parse(stoneNumW));//色戻す
                }
                
                errorflag1=true;
                Debug.Log("1に戻る：movableが0なのでNextmassを選んでも詰む");
            }else{
                Debug.Log(GData.movable.Count+"なので動ける");
            }
        }
    }
        void CheckNomovablemass(){//movableの色で詰んでないか
            //もしflagが立ってたらそもそも判定を行わない
            if(errorflag1){
                Debug.Log("既にエラーなので見送り");
            }else{
                int count=0;

                //確認とカウント
                if(turn==COLOR.BLACK){//Bのターン
                    for(int i=0;i<GData.movable.Count;i++){//movableの検索
                        if(IL2.GetMoveColor(1,GData.masscolorarray[GData.movable[i]])){
                            count++;
                            Debug.Log(IL2.GetMoveColor(1,GData.masscolorarray[GData.movable[i]])+"：移動可能"+count);
                        }
                    }
                }else if(turn==COLOR.WHITE){//Wのターン
                    for(int i=0;i<GData.movable.Count;i++){//movableの検索
                        if(IL2.GetMoveColor(2,GData.masscolorarray[GData.movable[i]])){
                            count++;
                        }
                    }
                }

                //行ける場所が1つもない=詰み
                if(count==0){
                    //点滅を戻す
                    if(turn==COLOR.BLACK){//Bのターン
                        CM1.Returnstonecolor1(int.Parse(stoneNumB));//色戻す
                    }else if(turn==COLOR.WHITE){//Wのターン
                        CM2.Returnstonecolor2(int.Parse(stoneNumW));//色戻す
                    }
                    
                    errorflag1=true;
                    Debug.Log("1に戻る：Nextmassを選んでもmovableカラーで詰むから");
                }

                count=0;//念の為初期化
            }
    }


    //-----------------------2
    void CheckTag2(){
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("2へ");
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
 
            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider.gameObject.CompareTag("mass")){//マスの時、座標を入手
                    NextmassNum = hit.collider.gameObject.name.Remove(0,5);
                    massPosition = hit.collider.gameObject.transform.position;

                    CollateMovablemass(int.Parse(NextmassNum));//移動可能なら""""進める""""
                    CollateMovableColor();//いろの判定。駄目なら2に戻す
                    if(sequence==3) MoveStonesequence();
                }
            }
        }
    }

    // 移動可能な場所の判定----------------------------------------------
    void CollateMovablemass(int x){//行けるならフラグが立つ。1と逆
        Debug.Log("検索します");
        for(int i=0;i<GData.movable.Count;i++){//movableの検索
            if(x==GData.movable[i]){
                sequence=3;//進める
                Debug.Log("3に進むmass");
            }
        }
    }
    void CollateMovableColor(){//移動先のカラーを確認
        if(turn==COLOR.BLACK){//Bのターン
            //Nextmassがmovableカラーかの確認
            if(!IL2.GetMoveColor(1,GData.masscolorarray[int.Parse(NextmassNum)])){//もし、Nextmassがmovableカラーかグレーじゃなければ、シークエンスを2に戻す
                Debug.Log("そこはmovableではありません");
                sequence=2;
            }
        }else if(turn==COLOR.WHITE){//Wのターン
            if(!IL2.GetMoveColor(2,GData.masscolorarray[int.Parse(NextmassNum)])) sequence=2;//もし、Nextmassがmovableカラーじゃなければ、シークエンスを2に戻す  
        }
        if(sequence==2) Debug.Log("3に進めない何かしらのエラーがあります");
    }

    //3の処理-------------------------------------------------------------

    void MoveStonesequence(){
        Debug.Log("次3へ");
        if(turn==COLOR.BLACK){//Bのターン
            CM1.SetStone(massPosition,int.Parse(stoneNumB));//見た目の移動

            //データ上の移動
            GData.massdataArray[int.Parse(NextmassNum)]=1;//移動先にplayerをセット

            GData.massdataArray[int.Parse(NowmassNum)]=0;//移動前のplayerを削除

            //ミルチェック
            MillCheckX(1,int.Parse(NextmassNum));
            MillCheckY(1,int.Parse(NextmassNum));
            
        }else if(turn==COLOR.WHITE){//Wのターン
            CM2.SetStone(massPosition,int.Parse(stoneNumW));//見た目の移動

            GData.massdataArray[int.Parse(NextmassNum)]=2;//移動先にplayerをセット

            GData.massdataArray[int.Parse(NowmassNum)]=0;//移動前のplayerを削除

            //ミルチェック
            MillCheckX(2,int.Parse(NextmassNum));
            MillCheckY(2,int.Parse(NextmassNum));

        }

        //ミルならミルに入る
        if(millflag){
            //ターンは交代しない

            //フェーズ移行
            sequence=36;
            Debug.Log("ミルの処理へ偏移します");
        }else{
            turnend();
        }
    }

    private void Mill(int player){
        string millstoneB=null;
        string millstoneW=null;
        string millmass=null;

        //みためON
        IL2.MillAnimation(true);

        if (Input.GetMouseButtonDown(0)) {
            
            //turnが0ならホントは2だから修正
            if(player==0)   player=2;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            foreach (RaycastHit hit in Physics.RaycastAll(ray)){
                if(hit.collider.gameObject.CompareTag("Bstone")){
                    millstoneB = hit.collider.gameObject.name.Remove(0,6);
                }else if(hit.collider.gameObject.CompareTag("mass")){
                    millmass = hit.collider.gameObject.name.Remove(0,5);
                }else if(hit.collider.gameObject.CompareTag("Wstone")){
                    millstoneW = hit.collider.gameObject.name.Remove(0,6);
                }
            }

            //update内のため、勝手に再起するからエラーの時はリセットするだけでOK
            if(player==1){
                if(millstoneB==null||millstoneW!=null||millmass==null){//駄目な場合
                    millstoneW=null;
                    millmass=null;
                    sequence=36;//シーケンスナンバーミル
                    Debug.Log("もう一回");
                }else{//ミル実行！
                    //データを消す
                    GData.massdataArray[int.Parse(millmass)]=0;//移動前のplayerを削除

                    //見た目でも消す
                    CM1.millMove(int.Parse(millstoneB));

                    //加点処理
                    Bscore++;

                    //UI反映
                    IL2.AddScore(1,Bscore);

                    //フェーズ終了判定
                    if(Bscore==3)   gameset(1);

                    //フラグを下げる
                    millflag=false;

                    //みためOFF
                    IL2.MillAnimation(false);

                    turnend();
                }
            }else if(player==2){
                if(millstoneW==null||millstoneB!=null||millmass==null){
                    millstoneB=null;
                    millmass=null;
                    sequence=36;
                    Debug.Log("もう一回");
                }else{//ミル実行！
                    //データを消す
                    GData.massdataArray[int.Parse(millmass)]=0;//移動前のplayerを削除

                    //見た目でも消す
                    CM2.millMove(int.Parse(millstoneW));

                    //加点処理
                    Wscore++;

                    //UI反映
                    IL2.AddScore(2,Wscore);

                    //フェーズ終了判定
                    if(Wscore==3)   gameset(2);

                    //フラグを下げる
                    millflag=false;
                    
                    //みためOFF
                    IL2.MillAnimation(false);

                    turnend();
                }
            }
        }
    }



        //ミルの確認仮(プレイヤー番号：移動した先のマスの番号)
        //横が並ぶ条件:奇数の場合真ん中のマスが確定するから横をミル
    private void MillCheckX(int player,int x){
        if(x%2==1){//奇数の場合
            if(x%6==1||x%6==3){//1か3なら/_で通常処理
                if(GData.massdataArray[x-1]==player&&GData.massdataArray[x]==player&&GData.massdataArray[x+1]==player)    millflag=true;//Debug.Log("X/か_ミル！");
            }else{// \の時は特殊
                if(GData.massdataArray[x-1]==player&&GData.massdataArray[x]==player&&GData.massdataArray[x-4]==player)    millflag=true;//Debug.Log("X\\ミル！");
            }
        }else{
            //x&6で場所の特定、それ%3で型の特定

            if(x % 6==0){//^
                if(GData.massdataArray[x]==player&&GData.massdataArray[x+1]==player&&GData.massdataArray[x+2]==player)    millflag=true;//Debug.Log("X/ミル！");
                if(GData.massdataArray[x]==player&&GData.massdataArray[x+5]==player&&GData.massdataArray[x+4]==player)    millflag=true;//Debug.Log("X\\ミル！");
            }else if(x % 6==2){//>
                if(GData.massdataArray[x]==player&&GData.massdataArray[x-1]==player&&GData.massdataArray[x-2]==player)    millflag=true;//Debug.Log("X/ミル！");  
                if(GData.massdataArray[x]==player&&GData.massdataArray[x+1]==player&&GData.massdataArray[x+2]==player)    millflag=true;//Debug.Log("X_ミル！");          
            }else if(x % 6==4){//<
                if(GData.massdataArray[x]==player&&GData.massdataArray[x+1]==player&&GData.massdataArray[x-4]==player)    millflag=true;//Debug.Log("X\\ミル！");
                if(GData.massdataArray[x]==player&&GData.massdataArray[x-1]==player&&GData.massdataArray[x-2]==player)    millflag=true;//Debug.Log("X_ミル！");   
            }
        }
        
        Debug.Log("チェック終了x");
    }

    //ミルの確認仮(プレイヤー番号：移動先のマス番号)
    //2と1が二重判定になりそう
    private void MillCheckY(int player,int x){
        //縦-が並ぶのは012か123しかない。1と4は処理が確定
        if(x/6==0){
            if(GData.massdataArray[x]==player&&GData.massdataArray[x+6]==player&&GData.massdataArray[x+12]==player)    millflag=true;//Debug.Log("Y012ミル！");
        }else if(x/6==1){
            if(GData.massdataArray[x-6]==player&&GData.massdataArray[x]==player&&GData.massdataArray[x+6]==player)    millflag=true;//Debug.Log("Y012ミル！");
            if(GData.massdataArray[x]==player&&GData.massdataArray[x+6]==player&&GData.massdataArray[x+12]==player)    millflag=true;//Debug.Log("Y123ミル！");
        }else if(x/6==2){
            if(GData.massdataArray[x-12]==player&&GData.massdataArray[x-6]==player&&GData.massdataArray[x]==player)    millflag=true;//Debug.Log("Y012ミル！");
            if(GData.massdataArray[x-6]==player&&GData.massdataArray[x]==player&&GData.massdataArray[x+6]==player)    millflag=true;//Debug.Log("Y123ミル！");
        }else if(x/6==3){
            if(GData.massdataArray[x]==player&&GData.massdataArray[x-6]==player&&GData.massdataArray[x-12]==player)    millflag=true;//Debug.Log("Y123ミル！");
        }
        
        Debug.Log("チェック終了y");
    }

    //ゲーム終了関数
    void gameset(int winplayer){//1、Bの勝利,2Wの勝利,12スコア勝ちの判定
        this.GetComponent<PlayerLogic2>().enabled = false;
        Debug.Log("ゲームセット!!!");
        if(winplayer==1)    winner=Color.black;
        else if(winplayer==2) winner=Color.white;
        else if(winplayer==12)  winner=Color.gray;
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene("Result");
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え時に呼ばれる
        // シーン切り替え後のスクリプトを取得
        var gameManager= GameObject.FindWithTag("ResultManager").GetComponent<Result>();
        
        // データを渡す処理
        gameManager.winner=winner;

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    //=========UI=========
    public void ReadyColorChange(){//統一、turnで変更を判定
        //アイコンの色を変更
        IL2.initUIcolor(Nowturn());
        //ボタンにメソッドをアタッチ
        if(turn==COLOR.BLACK){//Bのターン
            if(GData.MycolorB==Color.red){
                BCCButton1.onClick.AddListener (ColorChangeG);
                BCCButton2.onClick.AddListener (ColorChangeB);
            }else if(GData.MycolorB==Color.green){
                BCCButton1.onClick.AddListener (ColorChangeR);
                BCCButton2.onClick.AddListener (ColorChangeB);
            }else if(GData.MycolorB==Color.blue){
                BCCButton1.onClick.AddListener (ColorChangeR);
                BCCButton2.onClick.AddListener (ColorChangeG);
            }
        }else if(turn==COLOR.WHITE){//Wのターン
            if(GData.MycolorW==Color.red){
                WCCButton1.onClick.AddListener (ColorChangeG);
                WCCButton2.onClick.AddListener (ColorChangeB);
            }else if(GData.MycolorW==Color.green){
                WCCButton1.onClick.AddListener (ColorChangeR);
                WCCButton2.onClick.AddListener (ColorChangeB);
            }else if(GData.MycolorW==Color.blue){
                WCCButton1.onClick.AddListener (ColorChangeR);
                WCCButton2.onClick.AddListener (ColorChangeG);
            }
        }
    }

    public void RemoveOnClick(){
        BCCButton1.onClick.RemoveAllListeners();
        BCCButton2.onClick.RemoveAllListeners();
        WCCButton1.onClick.RemoveAllListeners();
        WCCButton2.onClick.RemoveAllListeners();
    }
    public void ColorChangeR(){
        //Gdataの更新
        if(turn==COLOR.BLACK){//Bのターン
            GData.MycolorB=Color.red;
        }else if(turn==COLOR.WHITE){
            GData.MycolorW=Color.red;
        }
        
        //マイカラーの更新
        IL2.SettingMycolor();

        // 戻すボタンを実行(次のターンmycolorが更新されなくなる)
        //CCパネルを消すだけ
        IL2.CloseCC(Nowturn());

        turnend();
    }

    public void ColorChangeG(){
        //Gdataの更新
        if(turn==COLOR.BLACK){//Bのターン
            GData.MycolorB=Color.green;
        }else if(turn==COLOR.WHITE){
            GData.MycolorW=Color.green;
        }
        
        //マイカラーの更新
        IL2.SettingMycolor();

        // 戻すボタンを実行(次のターンmycolorが更新されなくなる)
        //CCパネルを消すだけ
        IL2.CloseCC(Nowturn());

        turnend();
    }

    public void ColorChangeB(){
        //Gdataの更新
        if(turn==COLOR.BLACK){//Bのターン
            GData.MycolorB=Color.blue;
        }else if(turn==COLOR.WHITE){//Wのターン
            GData.MycolorW=Color.blue;
        }
        
        //マイカラーの更新
        IL2.SettingMycolor();

        // 戻すボタンを実行(次のターンmycolorが更新されなくなる)
        //CCパネルを消すだけ
        IL2.CloseCC(Nowturn());

        turnend();

    }

    //ターン終了時の処理----------------------------------
    void turnend(){
        Debug.Log("ターン終了処理を行います");
        Debug.Log ("<color=red>" + Nowturn() + "</color>");

        //UI変更
        IL2.ChangeUIturn(Nowturn());

        if(turn==COLOR.WHITE){//Wターン終了時のみ。色消し
            if(Checklastgray()){//色消しの処理を行うか？
                CD.DeleteColor();
            }else{//マスがすべてグレーになってゲーム終了
                //勝者判定
                if(Bscore>Wscore)       gameset(1);
                else if(Bscore<Wscore)  gameset(2);
                else if(Bscore==Wscore) gameset(12);
            }

            if(stoneNumW!=null){
                //選択の点滅を戻す
                CM2.Returnstonecolor2(int.Parse(stoneNumW));
            }

        }else{//黒のターン
            if(stoneNumB!=null){//CCボタンで来た可能性もある
                //選択の点滅を戻す
                CM1.Returnstonecolor1(int.Parse(stoneNumB));
            }
        }

        //初期化
        stoneNumB =null;
        stoneNumW =null;
        NowmassNum =null;
        NextmassNum =null;
        errorflag1=false;
        
        //ボタンのオンクリックを解除
        RemoveOnClick();

        //フェーズリセット
        sequence=1;
        Debug.Log("1に戻る");

        //ターン交代
        turn = turn == COLOR.BLACK ? COLOR.WHITE : COLOR.BLACK;
        //movableの削除
        GData.movable.Clear();
    }

    bool Checklastgray(){
        int c=0;
        for(int i=0;i<24;i++){
            if(GData.masscolorarray[i]!=Color.gray){
                Debug.Log("カラーますあり");
                // return true;//グレーじゃないのが混じってればセーフ
            }else{
                c++;
            }
        }

        if(c==23){//マス全体の23/24ならゲーム終了   
            return false;
        }else{
            return true;
        }
    }


    //光らせる用の確認関数
    // void ShineCheck1(){
    //     if(turn==COLOR.BLACK){//Bのターン
    //         for(int i=0;i<24;i++){
    //             if(GData.masscolorarray[i]==GData.MycolorB&&GData.massdataArray[i]==1)  CM1.stoneshine1(i);//マスがマイカラーかつBの玉が乗っている場合
    //         }
    //     }else if(turn==COLOR.WHITE){//Wのターン
    //         GData.MycolorW=Color.blue;
    //     }

    // }

}