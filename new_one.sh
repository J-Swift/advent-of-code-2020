#!/usr/bin/env sh

readonly _name="${1:-}"

if [ -z "${_name}" ]; then
  echo "ERROR: must provide a name"
  exit 1
fi

readonly name="day_${_name}"

if [ -f "${name}" ] || [ -d "${name}" ]; then
  echo "ERROR: [${name}] already exists"
  exit 1
fi

mkdir -p "${name}/part_1"
cp -r day_01/part_1/* "${name}/part_1/" && cd "${name}" && code .

