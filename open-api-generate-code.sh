#!/bin/bash

npx @openapitools/openapi-generator-cli generate -g aspnetcore \
  --additional-properties aspnetCoreVersion=7.0 \
  --additional-properties classModifier=abstract \
  --additional-properties operationModifier=abstract \
  --additional-properties packageName=ExamPortal.API \
  --additional-properties packageTitle=ExamPortal.API \
  --additional-properties enumValueSuffix= \
  --additional-properties operationResultTask=true \
  --additional-properties useSeparateModelProject=true \
  -i api.yaml \
  -o .



 