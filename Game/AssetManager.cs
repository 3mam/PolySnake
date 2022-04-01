using System.Collections.Generic;
using Poly.Interface;

namespace Poly;

public static class AssetManager
{
  private static readonly IDictionary<AssetList, IActor> _db =
    new Dictionary<AssetList, IActor>();

  public static void AddActor(AssetList assetName, IActor actor)
    => _db.Add(assetName, actor);
  
  public static IActor GetActor(AssetList assetName)
  => _db[assetName];
}