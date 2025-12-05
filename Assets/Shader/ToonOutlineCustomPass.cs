using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[CreateAssetMenu(menuName = "CustomPasses/ToonOutlinePass")]
public class ToonOutlineCustomPass : CustomPass
{
    public Material passMaterial; // ใส่ Material ที่ใช้ shader ด้านล่าง
    [Range(0, 8)] public float scale = 1.0f; // ปรับความหนา outline (multiplier)
    protected override void Execute(CustomPassContext ctx)
    {
        if (passMaterial == null) return;

        // ส่งค่าที่จำเป็นไปยัง shader (สามารถต่อเติมได้)
        passMaterial.SetFloat("_OutlineScale", scale);
        passMaterial.SetMatrix("_CamInvProj", ctx.hdCamera.camera.cameraToWorldMatrix * ctx.hdCamera.camera.projectionMatrix.inverse); // optional
        // ล้างค่า stencil/depth ไม่จำเป็น — เราจะทำ fullscreen blit
        CoreUtils.DrawFullScreen(ctx.cmd, passMaterial, target: ctx.cameraColorBuffer);
    }
}
